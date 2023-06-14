using AventStack.ExtentReports.Gherkin.Model;
using Browser.Core.Framework;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.InteropServices;

// This class will be for opening and closing connections to the Selenium Grid servers with a dedicated automation user account
// (Not your own Windows account). See https://code.premierinc.com/docs/display/PQA/File+Share#FileShare-AccessingFileSharewithin.NETCodeUsingaDedicatedAutomationUserAccount
// for more info. The automation user account we will be using is named app_pqa_account1 and the password for this user account is
// lfczLQ0y798!gR5DZJ2t

// There are 2 ways to open and close connections to a server. The first is to use the Net Use command. We will not be using
// Net Use within automation, but you can see an example of Net Use in the bottom of these comments, the section titled
// "Manual Server Connections". Doing so would allow you to manually see/manipulate open/close connections for any manual
// debugging purposes. The second way, which is the code we will be adding inside this
// class file, will be using the .NET System.Runtime.InteropServices namespace. We then use a Win32 API called
// WNetUseConnection, or you can use WNetAddConnection.
// See: https://learn.microsoft.com/en-us/windows/win32/api/winnetwk/nf-winnetwk-wnetuseconnectiona?redirectedfrom=MSDN
// and https://learn.microsoft.com/en-us/windows/win32/api/winnetwk/nf-winnetwk-wnetaddconnection3a
// For further reading on these implementations, I provided the StackOverflow links within the below code above each class

// See connection closing issue here: https://code.premierinc.com/docs/display/PQA/Issues+File+Share#IssuesFileShare-Disposing/ClosingConnectionduringParallelExecutionCausesTesttoFail
// You can see that issue for yourself by taking the test method code at the very bottom of this class file and pasting that
// code into a test class within Wikipeda.UITest, then uncommenting the Dispose method of the
// NetworkShareAccesser class, then executing those tests in parallel. Because of this issue, we are going to 
// just call NetworkShareAccesser.Access directly instead of wrapping it inside of a Using statement (as shown in the 
// StackOverflow where we got the code from). This will skip the Dispose method, which would skip closing the connection.
// This is ok, as explained in the above link. Update: I completely removed the Dispose method from the NetworkShareAccesser
// class since we dont use it. You can see in all references of this class, all I do to open a connection is call 
// NetworkShareAccesser.Access(). It will open a connection for 10 minutes, more than enough time for the test engineer
// to download/add/edit/upload his file if test is executing on Selenium Grid. 10 minutes is the default timeout as explained here:
// https://code.premierinc.com/docs/display/PQA/Issues+File+Share#IssuesFileShare-Disposing/ClosingConnectionduringParallelExecutionCausesTesttoFail

//****************************************Manual Server Connections******************************//
// Manually you can test if your dummy windows account can connect to a server/file share by using the "net use"
// command within CMD. See: https://superuser.com/questions/727944/accessing-a-windows-share-with-a-different-username
// Syntax: net use <driveletter>: \\<server>\<sharename> /USER:<domain>\<username> <password> /PERSISTENT:YES
// Note: < driveletter >: is optional
// Example: net use \\c3dierpdevsel01\seleniumdownloads /USER:corp\app_pqa_account1 lfczLQ0y798!gR5DZJ2t
// If the above is successful, CMD will say "The command completed successfully".

// You can then further verify the connection by entering the below to copy files to that file share:
// copy C:\Users\mjohnsto\Desktop\blah.txt \\c3dierpdevsel01\seleniumdownloads /Z /Y
// Then check that the destination folder \\c3dierpdevsel01\seleniumdownloads contains blah.txt
// Or you can use RoboCopy to copy files: ROBOCOPY \\server-source\c$\VMExports\ C:\VMExports\ /E /COPY:DAT

// You may need to close a connection first if you already had it opened: net use <network location> /delete
// Example: net use \\fileservername\filesharename /del Or to delete all connections: net use * /del
// I have not gotten the above to delete a connection yet. Instead I just shut down workstationg then open workstation
// For example, net stop workstation /y and net start workstation
// See https://code.premierinc.com/docs/display/PQA/Issues+File+Share#IssuesFileShare-Solution.6 for that syntax

// You can also test this (connecting/disconnecting from servers using dummy user credentials) using the Windows
// Explorer GUI. From the Tools menu select Map network drive.... On the Map Network Drive dialog window there is a
// checkbox for "Connect using different credentials".
// Note: If you do not see the menu bar in Windows Explorer, press the ALT key to make it appear.
namespace Browser.Core.Framework
{





    //**************************************************Below Class Works***************************************//

    /// <summary>
    /// Provides access to a network share. MJ 2023/04/19: I shortened this method because we dont need the additional overloads 
    /// since we always send the domain name and username and password. And we are not using the Dispose.
    /// To see original method not shortened, scroll  below this method.
    /// https://stackoverflow.com/questions/659013/accessing-a-shared-file-unc-from-a-remote-non-trusted-domain-with-credentials
    /// </summary>
    public class NetworkShareAccesser
    {
        private string _remoteUncName;
        private string _remoteComputerName;

        public string RemoteComputerName
        {
            get
            {
                return this._remoteComputerName;
            }
            set
            {
                this._remoteComputerName = value;
                this._remoteUncName = @"\\" + this._remoteComputerName;
            }
        }

        public string UserName
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }

        #region Consts

        private const int RESOURCE_CONNECTED = 0x00000001;
        private const int RESOURCE_GLOBALNET = 0x00000002;
        private const int RESOURCE_REMEMBERED = 0x00000003;

        private const int RESOURCETYPE_ANY = 0x00000000;
        private const int RESOURCETYPE_DISK = 0x00000001;
        private const int RESOURCETYPE_PRINT = 0x00000002;

        private const int RESOURCEDISPLAYTYPE_GENERIC = 0x00000000;
        private const int RESOURCEDISPLAYTYPE_DOMAIN = 0x00000001;
        private const int RESOURCEDISPLAYTYPE_SERVER = 0x00000002;
        private const int RESOURCEDISPLAYTYPE_SHARE = 0x00000003;
        private const int RESOURCEDISPLAYTYPE_FILE = 0x00000004;
        private const int RESOURCEDISPLAYTYPE_GROUP = 0x00000005;

        private const int RESOURCEUSAGE_CONNECTABLE = 0x00000001;
        private const int RESOURCEUSAGE_CONTAINER = 0x00000002;


        private const int CONNECT_INTERACTIVE = 0x00000008;
        private const int CONNECT_PROMPT = 0x00000010;
        private const int CONNECT_REDIRECT = 0x00000080;
        private const int CONNECT_UPDATE_PROFILE = 0x00000001;
        private const int CONNECT_COMMANDLINE = 0x00000800;
        private const int CONNECT_CMD_SAVECRED = 0x00001000;

        private const int CONNECT_LOCALDRIVE = 0x00000100;

        #endregion

        #region Errors

        private const int NO_ERROR = 0;

        private const int ERROR_ACCESS_DENIED = 5;
        private const int ERROR_ALREADY_ASSIGNED = 85;
        private const int ERROR_BAD_DEVICE = 1200;
        private const int ERROR_BAD_NET_NAME = 67;
        private const int ERROR_BAD_PROVIDER = 1204;
        private const int ERROR_CANCELLED = 1223;
        private const int ERROR_EXTENDED_ERROR = 1208;
        private const int ERROR_INVALID_ADDRESS = 487;
        private const int ERROR_INVALID_PARAMETER = 87;
        private const int ERROR_INVALID_PASSWORD = 1216;
        private const int ERROR_MORE_DATA = 234;
        private const int ERROR_NO_MORE_ITEMS = 259;
        private const int ERROR_NO_NET_OR_BAD_PATH = 1203;
        private const int ERROR_NO_NETWORK = 1222;

        private const int ERROR_BAD_PROFILE = 1206;
        private const int ERROR_CANNOT_OPEN_PROFILE = 1205;
        private const int ERROR_DEVICE_IN_USE = 2404;
        private const int ERROR_NOT_CONNECTED = 2250;
        private const int ERROR_OPEN_FILES = 2401;

        #endregion

        #region PInvoke Signatures

        [DllImport("Mpr.dll")]
        private static extern int WNetUseConnection(
            IntPtr hwndOwner,
            NETRESOURCE lpNetResource,
            string lpPassword,
            string lpUserID,
            int dwFlags,
            string lpAccessName,
            string lpBufferSize,
            string lpResult
            );

        // https://learn.microsoft.com/en-us/windows/win32/api/winnetwk/nf-winnetwk-wnetcancelconnection2a
        [DllImport("Mpr.dll")]
        private static extern int WNetCancelConnection2(
            string lpName,
            int dwFlags,
            bool fForce
            );

        [StructLayout(LayoutKind.Sequential)]
        private class NETRESOURCE
        {
            public int dwScope = 0;
            public int dwType = 0;
            public int dwDisplayType = 0;
            public int dwUsage = 0;
            public string lpLocalName = "";
            public string lpRemoteName = "";
            public string lpComment = "";
            public string lpProvider = "";
        }

        #endregion

        private NetworkShareAccesser(string remoteComputerName, string userName, string password)
        {
            RemoteComputerName = remoteComputerName;
            UserName = userName;
            Password = password;

            this.ConnectToShare(this._remoteUncName, this.UserName, this.Password, false);
        }

        /// <summary>
        /// Creates a NetworkShareAccesser for the given computer name using the static domain/computer name, username and password
        /// </summary>
        public static NetworkShareAccesser Access()
        {
            string hubName = AppSettings.Config["HubName"];
            string computerName = hubName;
            string username = "app_pqa_account1";
            string password = "lfczLQ0y798!gR5DZJ2t";

            //string local = @"C:\seleniumdownloads";
            return new NetworkShareAccesser(computerName, "corp" + @"\" + username, password);
        }

        /// <summary>
        /// Creates a NetworkShareAccesser for the given computer name using the given domain/computer name, username and password
        /// </summary>
        /// <param name="remoteComputerName"></param>
        /// <param name="domainName"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static NetworkShareAccesser Access(string remoteComputerName, string username, string password, string domainName = "corp")
        {
            return new NetworkShareAccesser(remoteComputerName, domainName + @"\" + username, password);
        }

        private void ConnectToShare(string remoteUnc, string username, string password, bool promptUser)
        {
            NETRESOURCE nr = new NETRESOURCE
            {
                dwType = RESOURCETYPE_DISK,
                lpRemoteName = remoteUnc
            };

            int result;
            if (promptUser)
            {
                result = WNetUseConnection(IntPtr.Zero, nr, "", "", CONNECT_INTERACTIVE | CONNECT_PROMPT, null, null, null);
            }
            else
            {
                result = WNetUseConnection(IntPtr.Zero, nr, password, username, 0, null, null, null);
            }

            if (result != NO_ERROR)
            {
                throw new Win32Exception(result);
            }
        }
    }




















    //MJ 2023/04/19: Original Code...
    ///// Provides access to a network share.
    ///// https://stackoverflow.com/questions/659013/accessing-a-shared-file-unc-from-a-remote-non-trusted-domain-with-credentials
    ///// </summary>
    //public class NetworkShareAccesser : IDisposable
    //{
    //    private string _remoteUncName;
    //    private string _remoteComputerName;

    //    public string RemoteComputerName
    //    {
    //        get
    //        {
    //            return this._remoteComputerName;
    //        }
    //        set
    //        {
    //            this._remoteComputerName = value;
    //            this._remoteUncName = @"\\" + this._remoteComputerName;
    //        }
    //    }

    //    public string UserName
    //    {
    //        get;
    //        set;
    //    }
    //    public string Password
    //    {
    //        get;
    //        set;
    //    }

    //    #region Consts

    //    private const int RESOURCE_CONNECTED = 0x00000001;
    //    private const int RESOURCE_GLOBALNET = 0x00000002;
    //    private const int RESOURCE_REMEMBERED = 0x00000003;

    //    private const int RESOURCETYPE_ANY = 0x00000000;
    //    private const int RESOURCETYPE_DISK = 0x00000001;
    //    private const int RESOURCETYPE_PRINT = 0x00000002;

    //    private const int RESOURCEDISPLAYTYPE_GENERIC = 0x00000000;
    //    private const int RESOURCEDISPLAYTYPE_DOMAIN = 0x00000001;
    //    private const int RESOURCEDISPLAYTYPE_SERVER = 0x00000002;
    //    private const int RESOURCEDISPLAYTYPE_SHARE = 0x00000003;
    //    private const int RESOURCEDISPLAYTYPE_FILE = 0x00000004;
    //    private const int RESOURCEDISPLAYTYPE_GROUP = 0x00000005;

    //    private const int RESOURCEUSAGE_CONNECTABLE = 0x00000001;
    //    private const int RESOURCEUSAGE_CONTAINER = 0x00000002;


    //    private const int CONNECT_INTERACTIVE = 0x00000008;
    //    private const int CONNECT_PROMPT = 0x00000010;
    //    private const int CONNECT_REDIRECT = 0x00000080;
    //    private const int CONNECT_UPDATE_PROFILE = 0x00000001;
    //    private const int CONNECT_COMMANDLINE = 0x00000800;
    //    private const int CONNECT_CMD_SAVECRED = 0x00001000;

    //    private const int CONNECT_LOCALDRIVE = 0x00000100;

    //    #endregion

    //    #region Errors

    //    private const int NO_ERROR = 0;

    //    private const int ERROR_ACCESS_DENIED = 5;
    //    private const int ERROR_ALREADY_ASSIGNED = 85;
    //    private const int ERROR_BAD_DEVICE = 1200;
    //    private const int ERROR_BAD_NET_NAME = 67;
    //    private const int ERROR_BAD_PROVIDER = 1204;
    //    private const int ERROR_CANCELLED = 1223;
    //    private const int ERROR_EXTENDED_ERROR = 1208;
    //    private const int ERROR_INVALID_ADDRESS = 487;
    //    private const int ERROR_INVALID_PARAMETER = 87;
    //    private const int ERROR_INVALID_PASSWORD = 1216;
    //    private const int ERROR_MORE_DATA = 234;
    //    private const int ERROR_NO_MORE_ITEMS = 259;
    //    private const int ERROR_NO_NET_OR_BAD_PATH = 1203;
    //    private const int ERROR_NO_NETWORK = 1222;

    //    private const int ERROR_BAD_PROFILE = 1206;
    //    private const int ERROR_CANNOT_OPEN_PROFILE = 1205;
    //    private const int ERROR_DEVICE_IN_USE = 2404;
    //    private const int ERROR_NOT_CONNECTED = 2250;
    //    private const int ERROR_OPEN_FILES = 2401;

    //    #endregion

    //    #region PInvoke Signatures

    //    [DllImport("Mpr.dll")]
    //    private static extern int WNetUseConnection(
    //        IntPtr hwndOwner,
    //        NETRESOURCE lpNetResource,
    //        string lpPassword,
    //        string lpUserID,
    //        int dwFlags,
    //        string lpAccessName,
    //        string lpBufferSize,
    //        string lpResult
    //        );

    //    // https://learn.microsoft.com/en-us/windows/win32/api/winnetwk/nf-winnetwk-wnetcancelconnection2a
    //    [DllImport("Mpr.dll")]
    //    private static extern int WNetCancelConnection2(
    //        string lpName,
    //        int dwFlags,
    //        bool fForce
    //        );

    //    [StructLayout(LayoutKind.Sequential)]
    //    private class NETRESOURCE
    //    {
    //        public int dwScope = 0;
    //        public int dwType = 0;
    //        public int dwDisplayType = 0;
    //        public int dwUsage = 0;
    //        public string lpLocalName = "";
    //        public string lpRemoteName = "";
    //        public string lpComment = "";
    //        public string lpProvider = "";
    //    }

    //    #endregion

    //    /// <summary>
    //    /// Creates a NetworkShareAccesser for the given computer name. The user will be promted to enter credentials
    //    /// </summary>
    //    /// <param name="remoteComputerName"></param>
    //    /// <returns></returns>
    //    public static NetworkShareAccesser Access(string remoteComputerName)
    //    {
    //        return new NetworkShareAccesser(remoteComputerName);
    //    }

    //    /// <summary>
    //    /// Creates a NetworkShareAccesser for the given computer name using the given domain/computer name, username and password
    //    /// </summary>
    //    /// <param name="remoteComputerName"></param>
    //    /// <param name="domainOrComuterName"></param>
    //    /// <param name="userName"></param>
    //    /// <param name="password"></param>
    //    public static NetworkShareAccesser Access(string remoteComputerName, string domainOrComuterName, string userName, string password)
    //    {
    //        return new NetworkShareAccesser(remoteComputerName,
    //                                        domainOrComuterName + @"\" + userName,
    //                                        password);
    //    }

    //    /// <summary>
    //    /// Creates a NetworkShareAccesser for the given computer name using the given username (format: domainOrComputername\Username) and password
    //    /// </summary>
    //    /// <param name="remoteComputerName"></param>
    //    /// <param name="userName"></param>
    //    /// <param name="password"></param>
    //    public static NetworkShareAccesser Access(string remoteComputerName, string userName, string password)
    //    {
    //        return new NetworkShareAccesser(remoteComputerName,
    //                                        userName,
    //                                        password);
    //    }

    //    private NetworkShareAccesser(string remoteComputerName)
    //    {
    //        RemoteComputerName = remoteComputerName;

    //        this.ConnectToShare(this._remoteUncName, null, null, true);
    //    }

    //    private NetworkShareAccesser(string remoteComputerName, string userName, string password)
    //    {
    //        RemoteComputerName = remoteComputerName;
    //        UserName = userName;
    //        Password = password;

    //        this.ConnectToShare(this._remoteUncName, this.UserName, this.Password, false);
    //    }

    //    private void ConnectToShare(string remoteUnc, string username, string password, bool promptUser)
    //    {
    //        NETRESOURCE nr = new NETRESOURCE
    //        {
    //            dwType = RESOURCETYPE_DISK,
    //            lpRemoteName = remoteUnc
    //        };

    //        int result;
    //        if (promptUser)
    //        {
    //            result = WNetUseConnection(IntPtr.Zero, nr, "", "", CONNECT_INTERACTIVE | CONNECT_PROMPT, null, null, null);
    //        }
    //        else
    //        {
    //            result = WNetUseConnection(IntPtr.Zero, nr, password, username, 0, null, null, null);
    //        }

    //        if (result != NO_ERROR)
    //        {
    //            throw new Win32Exception(result);
    //        }
    //    }

    //    private void DisconnectFromShare(string remoteUnc)
    //    {
    //        // https://learn.microsoft.com/en-us/windows/win32/api/winnetwk/nf-winnetwk-wnetcancelconnection2a
    //        int result = WNetCancelConnection2(remoteUnc, CONNECT_UPDATE_PROFILE, false);

    //        if (result != NO_ERROR)
    //        {
    //            throw new Win32Exception(result);
    //        }
    //    }

    //    /// <summary>
    //    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    //    /// </summary>
    //    /// <filterpriority>2</filterpriority>
    //    public void Dispose()
    //    {

    //        this.DisconnectFromShare(this._remoteUncName);
    //    }
    //}






















    // Below Class Says "The network name cannot be found" no matter which syntax I use for the networkName parameter
    // using (new NetworkConnection(fileShare_WithServerNamee, new NetworkCredential(username_withdomain, password)))
    //{
    //    File.Copy(fileToBeCopied, fileShare_WithServerNamee + "blah.txt");
    //}
    // https://stackoverflow.com/questions/295538/how-to-provide-user-name-and-password-when-connecting-to-a-network-share
    public class NetworkConnection : IDisposable
    {
        string _networkName;

        public NetworkConnection(string networkName,
            NetworkCredential credentials)
        {
            _networkName = networkName;

            var netResource = new NetResource()
            {
                Scope = ResourceScope.GlobalNetwork,
                ResourceType = ResourceType.Disk,
                DisplayType = ResourceDisplaytype.Share,
                RemoteName = networkName
            };

            var userName = string.IsNullOrEmpty(credentials.Domain)
                ? credentials.UserName
                : string.Format(@"{0}\{1}", credentials.Domain, credentials.UserName);

            var result = WNetAddConnection2(
                netResource,
                credentials.Password,
                userName,
                0);

            if (result != 0)
            {
                throw new Win32Exception(result);
            }
        }

        ~NetworkConnection()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            WNetCancelConnection2(_networkName, 0, true);
        }

        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(NetResource netResource,
            string password, string username, int flags);

        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string name, int flags,
            bool force);
    }

    [StructLayout(LayoutKind.Sequential)]
    public class NetResource
    {
        public ResourceScope Scope;
        public ResourceType ResourceType;
        public ResourceDisplaytype DisplayType;
        public int Usage;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string LocalName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string RemoteName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Comment;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Provider;
    }

    public enum ResourceScope : int
    {
        Connected = 1,
        GlobalNetwork,
        Remembered,
        Recent,
        Context
    };

    public enum ResourceType : int
    {
        Any = 0,
        Disk = 1,
        Print = 2,
        Reserved = 8,
    }

    public enum ResourceDisplaytype : int
    {
        Generic = 0x0,
        Domain = 0x01,
        Server = 0x02,
        Share = 0x03,
        File = 0x04,
        Group = 0x05,
        Network = 0x06,
        Root = 0x07,
        Shareadmin = 0x08,
        Directory = 0x09,
        Tree = 0x0a,
        Ndscontainer = 0x0b
    }

    // Below Class Works but doesnt handle the dispose properly. Which results in test results not returning
    // For example, if I enter wrong username or password, it should print that to the test results, but it does 
    // not. It actually prints nothing to test results, as the teardown is never reached
    //using (new WNetConnection(@"\\10.91.4.57\seleniumdownloads", username_withdomain, password))
    //{
    //    File.Copy(fileToBeCopied, @"\\10.91.4.57\seleniumdownloads" + @"\blah.txt");
    //}
    // https://stackoverflow.com/questions/1363679/a-specified-logon-session-does-not-exist-it-may-already-have-been-terminated/1364198#1364198
    public class WNetConnection : IDisposable
    {
        public readonly string RemoteShare;

        public WNetConnection(string remoteHost, string remoteUser, string remotePassword)
        {
            Uri loc;
            if (!Uri.TryCreate(remoteHost, UriKind.Absolute, out loc) || loc.IsUnc == false)
                throw new ApplicationException("Not a valid UNC path: " + remoteHost);

            string auth = loc.Host;
            string[] segments = loc.Segments;

            // expected format is '\\machine\share'
            this.RemoteShare = String.Format(@"\\{0}\{1}", auth, segments[1].Trim('\\', '/'));

            this.Connect(remoteUser, remotePassword);
        }

        ~WNetConnection()
        {
            Disconnect();
        }

        public void Dispose()
        {
            Disconnect();
            GC.SuppressFinalize(this);
        }

        #region Win32 API...

        [StructLayout(LayoutKind.Sequential)]
        internal struct NETRESOURCE
        {
            public int dwScope;
            public int dwType;
            public int dwDisplayType;
            public int dwUsage;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpLocalName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpRemoteName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpComment;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpProvider;
        }

        [DllImport("mpr.dll", EntryPoint = "WNetAddConnection2W", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern int WNetAddConnection2(ref NETRESOURCE lpNetResource, string lpPassword, string lpUsername, Int32 dwFlags);

        [DllImport("mpr.dll", EntryPoint = "WNetCancelConnectionW", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern int WNetCancelConnection(string lpRemoteName, bool bForce);

        private const int RESOURCETYPE_ANY = 0x00000000;
        private const int RESOURCETYPE_DISK = 0x00000001;
        private const int CONNECT_INTERACTIVE = 0x00000008;
        private const int CONNECT_PROMPT = 0x00000010;
        private const int NO_ERROR = 0;

        void Connect(string remoteUser, string remotePassword)
        {
            NETRESOURCE ConnInf = new NETRESOURCE();
            ConnInf.dwScope = 0;
            ConnInf.dwType = RESOURCETYPE_DISK;
            ConnInf.dwDisplayType = 0;
            ConnInf.dwUsage = 0;
            ConnInf.lpRemoteName = this.RemoteShare;
            ConnInf.lpLocalName = null;
            ConnInf.lpComment = null;
            ConnInf.lpProvider = null;

            // user must be qualified 'authority\user'
            if (remoteUser.IndexOf('\\') < 0)
                remoteUser = String.Format(@"{0}\{1}", new Uri(RemoteShare).Host, remoteUser);

            int dwResult = WNetAddConnection2(ref ConnInf, remotePassword, remoteUser, 0);
            if (NO_ERROR != dwResult)
                throw new Win32Exception(dwResult);
        }

        void Disconnect()
        {
            int dwResult = WNetCancelConnection(this.RemoteShare, true);
            if (NO_ERROR != dwResult)
                throw new Win32Exception(dwResult);
        }

        #endregion
    }
}


// Below code can be used to see opening and closing connections in parallel. You must have a file on the desktop titled 
// blah.txt. You must use the original code from the NetworkShareAccesser class, not the one I shortened that we currently use



//using Browser.Core.Framework;
//using NUnit.Framework;
//using System.IO;


//namespace Wikipedia.UITest
//{
//    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows)]

//    [TestFixture]
//    public class ParallelConnections1 : TestBase
//    {
//        public ParallelConnections1(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
//        public ParallelConnections1(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
//                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
//        { }

//        [Test]
//        [Category("QuickTest")]
//        public void test1()
//        {
//            // I am including different values for fileshare_ because the different classes within NetworkUtils.cs 
//            // only work with certain syntax.
//            string fileShare_WithServerName = @"\\c3dierpdevsel01";
//            string fileShare_WithServerNameWithoutSlashes = @"c3dierpdevsel01";
//            string fileShare_WithServerNameAndFileShareName = @"\\c3dierpdevsel01\seleniumdownloads";
//            string fileShare_WithServerIP = @"\\10.91.4.57";
//            string fileShare_WithServerIPAndFileShareName = @"\\10.91.4.57\seleniumdownloads";

//            string username_withoutdomain = @"svc_code_ado";
//            // If you wanted to include the domain inside the username variable
//            string username_withdomain = @"corp\svc_code_ado";
//            string password = "Oy_MkfUX-=7!6s1Cub.w";
//            string username = "app_pqa_account1";
//            string password = "lfczLQ0y798!gR5DZJ2t";

//            string fileToBeCopied = @"C:\Users\mjohnsto\Desktop\blah.txt";

//            // Some of the classes within NetworkUtils.cs allow you to pass in the domain as a parameter
//            string domain = "corp";


//            using (NetworkShareAccesser.Access(fileShare_WithServerNameWithoutSlashes, domain, username_withoutdomain, password))
//            {
//                File.Copy(fileToBeCopied, fileShare_WithServerNameAndFileShareName + @"\test1_1.txt");
//                //File.Delete(fileShare_WithServerNameAndFileShareName + @"\test1_1.txt");
//            };

//            using (NetworkShareAccesser.Access(fileShare_WithServerNameWithoutSlashes, domain, username_withoutdomain, password))
//            {
//                File.Copy(fileToBeCopied, fileShare_WithServerNameAndFileShareName + @"\test1_2.txt");
//                //File.Delete(fileShare_WithServerNameAndFileShareName + @"\test1_2.txt");
//            };

//            using (NetworkShareAccesser.Access(fileShare_WithServerNameWithoutSlashes, domain, username_withoutdomain, password))
//            {
//                File.Copy(fileToBeCopied, fileShare_WithServerNameAndFileShareName + @"\test1_3.txt");
//                // File.Delete(fileShare_WithServerNameAndFileShareName + @"\test1_3.txt");
//            };

//        }
//    }





//    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows)]

//    [TestFixture]
//    public class ParallelConnections2 : TestBase
//    {

//        public ParallelConnections2(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
//        public ParallelConnections2(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
//                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
//        { }

//        [Test]
//        [Category("QuickTest")]
//        public void test2()
//        {
//            // I am including different values for fileshare_ because the different classes within NetworkUtils.cs 
//            // only work with certain syntax.
//            string fileShare_WithServerName = @"\\c3dierpdevsel01";
//            string fileShare_WithServerNameWithoutSlashes = @"c3dierpdevsel01";
//            string fileShare_WithServerNameAndFileShareName = @"\\c3dierpdevsel01\seleniumdownloads";
//            string fileShare_WithServerIP = @"\\10.91.4.57";
//            string fileShare_WithServerIPAndFileShareName = @"\\10.91.4.57\seleniumdownloads";

//            string username_withoutdomain = @"svc_code_ado";
//            // If you wanted to include the domain inside the username variable
//            string username_withdomain = @"corp\svc_code_ado";
//            string password = "Oy_MkfUX-=7!6s1Cub.w";
//            string username = "app_pqa_account1";
//            string password = "lfczLQ0y798!gR5DZJ2t";

//            string fileToBeCopied = @"C:\Users\mjohnsto\Desktop\blah.txt";

//            // Some of the classes within NetworkUtils.cs allow you to pass in the domain as a parameter
//            string domain = "corp";


//            using (NetworkShareAccesser.Access(fileShare_WithServerNameWithoutSlashes, domain, username_withoutdomain, password))
//            {
//                File.Copy(fileToBeCopied, fileShare_WithServerNameAndFileShareName + @"\test2_1.txt");
//                //File.Delete(fileShare_WithServerNameAndFileShareName + @"\test2_1.txt");
//            };

//            using (NetworkShareAccesser.Access(fileShare_WithServerNameWithoutSlashes, domain, username_withoutdomain, password))
//            {
//                File.Copy(fileToBeCopied, fileShare_WithServerNameAndFileShareName + @"\test2_2.txt");
//                //File.Delete(fileShare_WithServerNameAndFileShareName + @"\test2_2.txt");
//            };

//            using (NetworkShareAccesser.Access(fileShare_WithServerNameWithoutSlashes, domain, username_withoutdomain, password))
//            {
//                File.Copy(fileToBeCopied, fileShare_WithServerNameAndFileShareName + @"\test2_3.txt");
//                //File.Delete(fileShare_WithServerNameAndFileShareName + @"\test2_3.txt");
//            };

//        }
//    }







//    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows)]

//    [TestFixture]
//    public class ParallelConnections3 : TestBase
//    {

//        public ParallelConnections3(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
//        public ParallelConnections3(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
//                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
//        { }

//        [Test]
//        [Category("QuickTest")]
//        public void test3()
//        {
//            // I am including different values for fileshare_ because the different classes within NetworkUtils.cs 
//            // only work with certain syntax.
//            string fileShare_WithServerName = @"\\c3dierpdevsel01";
//            string fileShare_WithServerNameWithoutSlashes = @"c3dierpdevsel01";
//            string fileShare_WithServerNameAndFileShareName = @"\\c3dierpdevsel01\seleniumdownloads";
//            string fileShare_WithServerIP = @"\\10.91.4.57";
//            string fileShare_WithServerIPAndFileShareName = @"\\10.91.4.57\seleniumdownloads";

//            string username_withoutdomain = @"svc_code_ado";
//            // If you wanted to include the domain inside the username variable
//            string username_withdomain = @"corp\svc_code_ado";
//            string password = "Oy_MkfUX-=7!6s1Cub.w";

//            string username = "app_pqa_account1";
//            string password = "lfczLQ0y798!gR5DZJ2t";

//            string fileToBeCopied = @"C:\Users\mjohnsto\Desktop\blah.txt";

//            // Some of the classes within NetworkUtils.cs allow you to pass in the domain as a parameter
//            string domain = "corp";


//            using (NetworkShareAccesser.Access(fileShare_WithServerNameWithoutSlashes, domain, username_withoutdomain, password))
//            {
//                File.Copy(fileToBeCopied, fileShare_WithServerNameAndFileShareName + @"\test3_1.txt");
//                //File.Delete(fileShare_WithServerNameAndFileShareName + @"\test3_1.txt");
//            };

//            using (NetworkShareAccesser.Access(fileShare_WithServerNameWithoutSlashes, domain, username_withoutdomain, password))
//            {
//                File.Copy(fileToBeCopied, fileShare_WithServerNameAndFileShareName + @"\test3_2.txt");
//                //File.Delete(fileShare_WithServerNameAndFileShareName + @"\test3_2.txt");
//            };

//            using (NetworkShareAccesser.Access(fileShare_WithServerNameWithoutSlashes, domain, username_withoutdomain, password))
//            {
//                File.Copy(fileToBeCopied, fileShare_WithServerNameAndFileShareName + @"\test3_3.txt");
//                //File.Delete(fileShare_WithServerNameAndFileShareName + @"\test3_3.txt");
//            };
//        }
//    }
// }


