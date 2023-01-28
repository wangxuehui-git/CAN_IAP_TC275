using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using vxlapi_NET;


//1.ZLGCAN系列接口卡信息的数据类型。
public struct VCI_BOARD_INFO 
{ 
	public UInt16 hw_Version;
    public UInt16 fw_Version;
    public UInt16 dr_Version;
    public UInt16 in_Version;
    public UInt16 irq_Num;
    public byte can_Num;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=20)] public byte []str_Serial_Num;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
    public byte[] str_hw_Type;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public byte[] Reserved;
}


/////////////////////////////////////////////////////
//2.定义CAN信息帧的数据类型。
unsafe public struct VCI_CAN_OBJ  //使用不安全代码
{
    public uint ID;
    public uint TimeStamp;
    public byte TimeFlag;
    public byte SendType;
    public byte RemoteFlag;//是否是远程帧
    public byte ExternFlag;//是否是扩展帧
    public byte DataLen;

    public fixed byte Data[8];

    public fixed byte Reserved[3];

}
////2.定义CAN信息帧的数据类型。
//public struct VCI_CAN_OBJ 
//{
//    public UInt32 ID;
//    public UInt32 TimeStamp;
//    public byte TimeFlag;
//    public byte SendType;
//    public byte RemoteFlag;//是否是远程帧
//    public byte ExternFlag;//是否是扩展帧
//    public byte DataLen;
//    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//    public byte[] Data;
//    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
//    public byte[] Reserved;

//    public void Init()
//    {
//        Data = new byte[8];
//        Reserved = new byte[3];
//    }
//}

//3.定义CAN控制器状态的数据类型。
public struct VCI_CAN_STATUS 
{
    public byte ErrInterrupt;
    public byte regMode;
    public byte regStatus;
    public byte regALCapture;
    public byte regECCapture;
    public byte regEWLimit;
    public byte regRECounter;
    public byte regTECounter;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[] Reserved;
}

//4.定义错误信息的数据类型。
public struct VCI_ERR_INFO 
{
    public UInt32 ErrCode;
    public byte Passive_ErrData1;
    public byte Passive_ErrData2;
    public byte Passive_ErrData3;
    public byte ArLost_ErrData;
}

//5.定义初始化CAN的数据类型
public struct VCI_INIT_CONFIG 
{
    public UInt32 AccCode;
    public UInt32 AccMask;
    public UInt32 Reserved;
    public byte Filter;
    public byte Timing0;
    public byte Timing1;
    public byte Mode;
}

public struct CHGDESIPANDPORT 
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public byte[] szpwd;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public byte[] szdesip;
    public Int32 desport;

    public void Init()
    {
        szpwd = new byte[10];
        szdesip = new byte[20];
    }
}


namespace CAN_IAP_ForUDS
{


    public partial class Main : Form
    {
        #region  CANalyzer
        // -----------------------------------------------------------------------------------------------
        // DLL Import for RX events
        // -----------------------------------------------------------------------------------------------
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int WaitForSingleObject(int handle, int timeOut);
        // -----------------------------------------------------------------------------------------------
        XLClass.xl_event_collection xlEventCollection = new XLClass.xl_event_collection(1);

        
        // -----------------------------------------------------------------------------------------------
        // Global variables
        // -----------------------------------------------------------------------------------------------
        // Driver access through XLDriver (wrapper)
        public static XLDriver CANDemo = new XLDriver();
        public static String appName = "CAN_IAP_ForUDS";

        // Driver configuration
        public static XLClass.xl_driver_config driverConfig = new XLClass.xl_driver_config();
        public static XLClass.xl_chip_params canparams = new XLClass.xl_chip_params();

        // Variables required by XLDriver
        public static XLDefine.XL_HardwareType hwType = XLDefine.XL_HardwareType.XL_HWTYPE_NONE;
        public static uint hwIndex = 0;
        public static uint hwChannel = 0;
        public static int portHandle = -1;
        public static int eventHandle = -1;
        public static UInt64 accessMask = 0;
        public static UInt64 permissionMask = 0;
        public static UInt64 txMask = 1;
        public static int channelIndex = 0;

        #endregion

        #region  ZLGCAN
        const int ZLG_USBCAN2 = 4;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DeviceType"></param>
        /// <param name="DeviceInd"></param>
        /// <param name="Reserved"></param>
        /// <returns></returns>
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_OpenDevice(UInt32 DeviceType, UInt32 DeviceInd, UInt32 Reserved);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_CloseDevice(UInt32 DeviceType, UInt32 DeviceInd);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_InitCAN(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_INIT_CONFIG pInitConfig);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_ReadBoardInfo(UInt32 DeviceType, UInt32 DeviceInd, ref VCI_BOARD_INFO pInfo);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_ReadErrInfo(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_ERR_INFO pErrInfo);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_ReadCANStatus(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_CAN_STATUS pCANStatus);

        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_GetReference(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, UInt32 RefType, ref byte pData);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_SetReference(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, UInt32 RefType, ref byte pData);

        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_GetReceiveNum(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_ClearBuffer(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);

        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_StartCAN(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_ResetCAN(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);

        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_Transmit(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_CAN_OBJ pSend, UInt32 Len);

        //[DllImport("controlcan.dll")]
        //static extern UInt32 VCI_Receive(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_CAN_OBJ pReceive, UInt32 Len, Int32 WaitTime);
        [DllImport("controlcan.dll", CharSet = CharSet.Ansi)]
        static extern UInt32 VCI_Receive(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, IntPtr pReceive, UInt32 Len, Int32 WaitTime);

        public class Main_GlobalData
        {
            public static UInt16 CAN_Set_Flag  = 0;
            public static UInt16 CAN_Open_Flag = 0;
            public static UInt32 Dev_Type = 4;
            public static UInt32 Dev_Index = 0;
            public static UInt32 CAN_Index = 0;

            public static UInt16 CAN_MCU_Type = 0;  //0: STM32  1: Freescale
            
        }
        VCI_CAN_OBJ[] m_recobj = new VCI_CAN_OBJ[50];
        VCI_CAN_OBJ[] sendobj = new VCI_CAN_OBJ[1];
        #endregion

        Thread IAP_Thread;
        byte[] SendData = new byte[8];
        byte[] ReadData = new byte[8];

        static string Save_Hex_Pacth = "E:\\";
        static byte   CAN_DATA_TYPE = 0;            /* 0 为标准帧 1：扩展帧 */

        //IAPDownload全局变量定义区
        
        public static byte   Stop_Data_Show_Flag = 1;
        static uint     gIAP_checkOk   = 0;
        static uint     gIAP_startFlag = 0;
        static string   hexFileName = "";
        static string[] Hex_FileLinesBuff;
        static UInt32   Hex_FileLineNum = 0;

        static byte IAP_Status = 0;         // 下载流程状态
        static byte IAP_PageIndex = 0;      // 下载块标号
        static byte IAP_PageIndexSum = 2;   // 下载块总数
        static byte BT_mode = 0;            // 下载方式类型 0: 广播 1:单点

        static UInt32[] IAP_P2BSendPageBuf = new UInt32[10000];  //页地址列表
        static UInt32   IAP_P2BSendPageSum = 0;                  //页总数
        static UInt32   IAP_P2BSendPageIndex = 0;                //页Send 计数器
        static byte[]   IAP_P2BSendDataAry = new byte[65535];    //Send 单页数据列表
        static UInt16   IAP_P2BSendDataIndex = 0;                //Send 单页数据

        static UInt32   IAP_P2BSingleNodeAddr = 0;          //单点下载节点地址   
        static byte     IAP_B2PNodeAddrCnt = 0;             //响应的节点地址数
        static string   IAP_B2PNodeSoftVer = "";            //响应的节点地软件版本号

        static UInt32   IAP_P2BDataByteSum = 0;      //Send 数据总字节数
        static Byte     IAP_P2BSendIndex = 0;        //Send 数据Index(0x00~0xFF) 
        static UInt16   IAP_P2BLine_index = 0;       //Send 数据行
        static UInt16   IAP_P2BPagesend_index = 0;   //Send 页数据中的Index(0~32)
        static UInt16   IAP_P2BSendTimerCnt  = 0;    //Send 命令超时计数器
        static UInt16   IAP_P2BPageData_Length = 0;      //每页数据长度    
           
        static UInt16   IAP_B2PData_Length = 0;
        //*****************************************************************************
        // These defines are used to define the range of values that are used for
        // CAN update with UDS protocol.
        //*****************************************************************************
        static UInt32 CAN_UDS_FUNC_ADDR = 0x7E1;
        static UInt32 CAN_UDS_PHYS_ADDR = 0x7FF;
        static UInt32 CAN_UDS_SEND_ADDR = 0;
        static UInt32 CAN_UDS_PHYS_RESP = 0x7E9;
      
        static byte   CAN_UDS_POS_RESP_Flag = 0;    /* UDS 正响应禁止标志位 */
        static byte   CAN_UDS_RESP_State = 0;       /* UDS 响应状态标志位 */
        static byte   CAN_UDS_NEG_Flag = 0;
        static UInt16 CAN_UDS_ReadDataSum = 0;
        static UInt16 CAN_UDS_ReadDataLen = 0;


        public class UDS_Serv_ID_t
        {
            public static Byte UDS_SERV_ID__NONE = 0x00,

            /* Section 9, Diagnostic and communication management functional unit */
            UDS_SERV_ID__DIAG_SESS_CTRL = 0x10,
            UDS_SERV_ID__ECU_RESET = 0x11,
            UDS_SERV_ID__SEC_ACC = 0x27,
            UDS_SERV_ID__COMM_CTRL = 0x28,
            UDS_SERV_ID__TSTR_PRESENT = 0x3E,
            UDS_SERV_ID__ACC_TIMING_PARAM = 0x83,
            UDS_SERV_ID__SEC_DATA_TRANS = 0x84,
            UDS_SERV_ID__CTRL_DTC_SETTING = 0x85,
            UDS_SERV_ID__RSPD_ON_EV = 0x86,
            UDS_SERV_ID__LINK_CTRL = 0x87,

            /* Section 10, Data transmission functional unit */
            UDS_SERV_ID__READ_DATA_BY_ID = 0x22,
            UDS_SERV_ID__READ_MEM_BY_ADDR = 0x23,
            UDS_SERV_ID__READ_SCALING_DATA_BY_ID = 0x24,
            UDS_SERV_ID__READ_DATA_BY_PERIODIC_ID = 0x2A,
            UDS_SERV_ID__DYN_DEF_DATA_ID = 0x2C,
            UDS_SERV_ID__WRITE_DATA_BY_ID = 0x2E,
            UDS_SERV_ID__WRITE_MEM_BY_ADDR = 0x3D,

            /* Section 11, Stored data transmission functional unit */
            UDS_SERV_ID__CLR_DIAG_INFO = 0x14,
            UDS_SERV_ID__READ_DTC_INFO = 0x19,

            /* Section 12, InputOutput control functional unit */
            UDS_SERV_ID__IN_OUT_CTRL_BY_ID = 0x2F,

            /* Section 13, Remote activation of routine functional unit */
            UDS_SERV_ID__ROUTINE_CTRL = 0x31,

            /* Section 14, Upload download functional unit */
            UDS_SERV_ID__RQST_DOWNLOAD = 0x34,
            UDS_SERV_ID__RQST_UPLOAD = 0x35,
            UDS_SERV_ID__TRANSFER_DATA = 0x36,
            UDS_SERV_ID__RQST_TRANSFER_EXIT = 0x37;
        }   
      
        public class UDS_Pos_Resp_Code_t
        {
            public static Byte UDS_POS_RESP__NONE = 0x00,

            /* Section 9, Diagnostic and communication management functional unit */
            UDS_POS_RESP__DIAG_SESS_CTRL = 0x50,
            UDS_POS_RESP__ECU_RESET = 0x51,
            UDS_POS_RESP__SEC_ACC = 0x67,
            UDS_POS_RESP__COMM_CTRL = 0x68,
            UDS_POS_RESP__TSTR_PRESENT = 0x7E,
            UDS_POS_RESP__ACC_TIMING_PARAM = 0xC3,
            UDS_POS_RESP__SEC_DATA_TRANS = 0xC4,
            UDS_POS_RESP__CTRL_DTC_SETTING = 0xC5,
            UDS_POS_RESP__RSPD_ON_EV = 0xC6,
            UDS_POS_RESP__LINK_CTRL = 0xC7,

            /* Section 10, Data transmission functional unit */
            UDS_POS_RESP__READ_DATA_BY_ID = 0x62,
            UDS_POS_RESP__READ_MEM_BY_ADDR = 0x63,
            UDS_POS_RESP__READ_SCALING_DATA_BY_ID = 0x64,
            UDS_POS_RESP__READ_DATA_BY_PERIODIC_ID = 0x6A,
            UDS_POS_RESP__DYN_DEF_DATA_ID = 0x6C,
            UDS_POS_RESP__WRITE_DATA_BY_ID = 0x6E,
            UDS_POS_RESP__WRITE_MEM_BY_ADDR = 0x7D,

            /* Section 11, Stored data transmission functional unit */
            UDS_POS_RESP__CLR_DIAG_INFO = 0x54,
            UDS_POS_RESP__READ_DTC_INFO = 0x59,

            /* Section 12, InputOutput control functional unit */
            UDS_POS_RESP__IN_OUT_CTRL_BY_ID = 0x6F,

            /* Section 13, Remote activation of routine functional unit */
            UDS_POS_RESP__ROUTINE_CTRL = 0x71,

            /* Section 14, Upload download functional unit */
            UDS_POS_RESP__RQST_DOWNLOAD = 0x74,
            UDS_POS_RESP__RQST_UPLOAD = 0x75,
            UDS_POS_RESP__TRANSFER_DATA = 0x76,
            UDS_POS_RESP__RQST_TRANSFER_EXIT = 0x77;
        }

        public class UDS_Neg_Resp_Code_t
        {
            public static Byte UDS_NEG_RESP_CODE__PR = 0X00, /* POS_RESP */
            UDS_NEG_RESP_CODE__GR = 0X10, /* GEN_REJECT */
            UDS_NEG_RESP_CODE__SNS = 0X11, /* SERV_NOT_SUP */
            UDS_NEG_RESP_CODE__SFNS = 0X12, /* SUB_FCTN_NOT_SUP */
            UDS_NEG_RESP_CODE__IMLOIF = 0X13, /* INCORRECT_MSG_LEN_OR_INVALID_FORMAT */
            UDS_NEG_RESP_CODE__RTL = 0X14, /* RESP_TOO_LONG */
            UDS_NEG_RESP_CODE__BRR = 0X21, /* BUSY_REPEAT_RQST */
            UDS_NEG_RESP_CODE__CNC = 0X22, /* CONDITIONS_NOT_CORRECT */
            UDS_NEG_RESP_CODE__RSE = 0X24, /* RQST_SEQ_ERR */
            UDS_NEG_RESP_CODE__NRFSC = 0X25, /* NO_RESP_FROM_SUBNET_COMPONENT */
            UDS_NEG_RESP_CODE__FPEORA = 0X26, /* FAILURE_PREVENTS_EXECUTION_OF_RQST_ACT */
            UDS_NEG_RESP_CODE__ROOR = 0X31, /* RQST_OUT_OF_RANGE */
            UDS_NEG_RESP_CODE__SAD = 0X33, /* SEC_ACC_DENIED */
            UDS_NEG_RESP_CODE__IK = 0X35, /* INVALID_KEY */
            UDS_NEG_RESP_CODE__ENOA = 0X36, /* EXCEEDED_NUM_OF_ATTEMPTS */
            UDS_NEG_RESP_CODE__RTDNE = 0X37, /* REQD_TIME_DELAY_NOT_EXPIRED */
            UDS_NEG_RESP_CODE__UDNA = 0X70, /* UP_LOAD_DOWN_LOAD_NOT_ACCEPTED */
            UDS_NEG_RESP_CODE__TDS = 0X71, /* TRANSFER_DATA_SPND */
            UDS_NEG_RESP_CODE__GPF = 0X72, /* GEN_PRG_FAILURE */
            UDS_NEG_RESP_CODE__WBSC = 0X73, /* WRONG_BLK_SEQ_CNTR */
            UDS_NEG_RESP_CODE__RCRRP = 0X78, /* RQST_CORRECTLY_RX_RESP_PENDING */
            UDS_NEG_RESP_CODE__SFNSIAS = 0X7E, /* SUB_FCTN_NOT_SUP_IN_ACTV_SESS */
            UDS_NEG_RESP_CODE__SNSIAS = 0X7F, /* SERV_NOT_SUP_IN_ACTV_SESS */
            UDS_NEG_RESP_CODE__RPMTH = 0X81, /* RPM_TOO_HI */
            UDS_NEG_RESP_CODE__RPMTL = 0X82, /* RPM_TOO_LOW */
            UDS_NEG_RESP_CODE__EIR = 0X83, /* ENGINE_IS_RUNNING */
            UDS_NEG_RESP_CODE__EINR = 0X84, /* ENGINE_IS_NOT_RUNNING */
            UDS_NEG_RESP_CODE__ERTTL = 0X85, /* ENGINE_RUN_TIME_TOO_LOW */
            UDS_NEG_RESP_CODE__TEMPTH = 0X86, /* TEMP_TOO_HI */
            UDS_NEG_RESP_CODE__TEMPTL = 0X87, /* TEMP_TOO_LOW */
            UDS_NEG_RESP_CODE__VSTH = 0X88, /* VEH_SPD_TOO_HI */
            UDS_NEG_RESP_CODE__VSTL = 0X89, /* VEH_SPD_TOO_LOW */
            UDS_NEG_RESP_CODE__TPTH = 0X8A, /* THROTTLE_PEDAL_TOO_HI */
            UDS_NEG_RESP_CODE__TPTL = 0X8B, /* THROTTLE_PEDAL_TOO_LOW */
            UDS_NEG_RESP_CODE__TRNIN = 0X8C, /* TRANS_RANGE_NOT_IN_NEUTRAL */
            UDS_NEG_RESP_CODE__TRNIG = 0X8D, /* TRANS_RANGE_NOT_IN_GEAR */
            UDS_NEG_RESP_CODE__BSNC = 0X8F, /* BRAKE_SWITCH_NOT_CLOSED */
            UDS_NEG_RESP_CODE__SLNIP = 0X90, /* SHIFT_LEVER_NOT_IN_PARK */
            UDS_NEG_RESP_CODE__TCCL = 0X91, /* TORQUE_CONVERTER_CLUTCH_LOCKED */
            UDS_NEG_RESP_CODE__VTH = 0X92, /* VOLTAGE_TOO_HI */
            UDS_NEG_RESP_CODE__VTL = 0X93; /* VOLTAGE_TOO_LOW */
          }

        public class UDS_Diag_Sess_Type_t
        {
            public static Byte UDS_DIAG_SESS_TYPE__DFLT_SESS = 0x01,    /* defaultSession */
            UDS_DIAG_SESS_TYPE__PGM_SESS = 0X02,    /* PRGS */
            UDS_DIAG_SESS_TYPE__EXTD_DIAG_SESS = 0X03,    /* EXTDS */
            UDS_DIAG_SESS_TYPE__SAFTEY_SYS_DIAG_SESS = 0X04;    /* SSDS  */
        }

        static UInt32[] HEX_StarAddress = { 0x80080000, 0x80340000 };
        static UInt32[] HEX_PageSize = { 0x100000, 0x40000 };
        static UInt32[] HEX_TransferSize = { 0, 0 };
        static UInt32[] HEX_StarIndex = { 0 , 0};
        static UInt32[] HEX_EndIndex = { 0, 0 };

        public Main()
        {
            InitializeComponent();
        }

        unsafe private void timer_rec_Tick(object sender, EventArgs e)
        {
            if (Main.Main_GlobalData.Dev_Type != 0)
            {
                UInt32 res = new UInt32();
                res = VCI_GetReceiveNum(Main_GlobalData.Dev_Type, Main_GlobalData.Dev_Index, Main_GlobalData.CAN_Index);
                if (res == 0)
                    return;
                //res = VCI_Receive(Main_GlobalData.Dev_Type, Main_GlobalData.Dev_Index, Main_GlobalData.CAN_Index, ref m_recobj[0],50, 100);

                /////////////////////////////////////
                UInt32 con_maxlen = 50;
                IntPtr pt = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(VCI_CAN_OBJ)) * (Int32)con_maxlen);




                res = VCI_Receive(Main_GlobalData.Dev_Type, Main_GlobalData.Dev_Index, Main_GlobalData.CAN_Index, pt, con_maxlen, 100);
                ////////////////////////////////////////////////////////

                String str = "";
                for (UInt32 i = 0; i < res; i++)
                {
                    VCI_CAN_OBJ obj = (VCI_CAN_OBJ)Marshal.PtrToStructure((IntPtr)((UInt32)pt + i * Marshal.SizeOf(typeof(VCI_CAN_OBJ))), typeof(VCI_CAN_OBJ));

                    str = "Read:";
                    str += "Frame Type:";
                    if (obj.ExternFlag == 0)
                        str += "STD ";
                    else
                        str += "EXT ";
                    if (obj.RemoteFlag == 0)
                        str += "DATA;";
                    else
                        str += "Remote;";

                    str += "Frame ID:0x" + System.Convert.ToString((Int32)obj.ID, 16);


                    //////////////////////////////////////////
                    if (obj.RemoteFlag == 0)
                    {
                        str += " Data:";
                        byte len = (byte)(obj.DataLen % 9);
                        byte j = 0;
                        if (j++ < len)
                            str += " " + System.Convert.ToString(obj.Data[0], 16);
                        if (j++ < len)
                            str += " " + System.Convert.ToString(obj.Data[1], 16);
                        if (j++ < len)
                            str += " " + System.Convert.ToString(obj.Data[2], 16);
                        if (j++ < len)
                            str += " " + System.Convert.ToString(obj.Data[3], 16);
                        if (j++ < len)
                            str += " " + System.Convert.ToString(obj.Data[4], 16);
                        if (j++ < len)
                            str += " " + System.Convert.ToString(obj.Data[5], 16);
                        if (j++ < len)
                            str += " " + System.Convert.ToString(obj.Data[6], 16);
                        if (j++ < len)
                            str += " " + System.Convert.ToString(obj.Data[7], 16);

                    }
                    if (Stop_Data_Show_Flag == 0)
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new CanSendDataShow(ShowSendData), new object[] { str });
                        }
                        else
                        {
                            ShowSendData(str);
                        }
                    }

                    //解析收到的数据
                    if (obj.ID == 0x00000000)
                    {
                        continue;
                    }
                    if (obj.ExternFlag != CAN_DATA_TYPE)
                    {
                        continue;
                    }
                    if ((obj.ID & 0x7FF) != CAN_UDS_PHYS_RESP)
                    {
                        continue;
                    }
                    for (uint jj = 0; jj < 8; jj++)
                    {
                        ReadData[jj] = obj.Data[jj];
                    }
                    CANData_Run(obj.ID, ReadData);                

                }
                Marshal.FreeHGlobal(pt);
            }
            else
            {
                // Result of XL Driver function calls
                XLDefine.XL_Status xlStatus = XLDefine.XL_Status.XL_SUCCESS;

                List<XLClass.xl_event> receivedEvents = new List<XLClass.xl_event>();
                int receivecnt = 500;
                xlStatus = CANDemo.XL_Receive(portHandle, receivecnt, ref  receivedEvents);
                if (xlStatus != XLDefine.XL_Status.XL_ERR_QUEUE_IS_EMPTY)
                {
                    for (int i = 0; i < receivedEvents.Count; i++)
                    {
                        if ((receivedEvents[i].flags & XLDefine.XL_MessageFlags.XL_EVENT_FLAG_OVERRUN) != 0)
                        {
                            continue;
                        }

                        // ...and data is a Rx msg...
                        if (receivedEvents[i].tag == XLDefine.XL_EventTags.XL_RECEIVE_MSG)
                        {
                            if ((receivedEvents[i].tagData.can_Msg.flags & XLDefine.XL_MessageFlags.XL_CAN_MSG_FLAG_OVERRUN) != 0)
                            {
                                continue;
                            }

                            // ...check various flags
                            if ((receivedEvents[i].tagData.can_Msg.flags & XLDefine.XL_MessageFlags.XL_CAN_MSG_FLAG_ERROR_FRAME)
                                == XLDefine.XL_MessageFlags.XL_CAN_MSG_FLAG_ERROR_FRAME)
                            {
                                continue;
                            }
                            else if ((receivedEvents[i].tagData.can_Msg.flags & XLDefine.XL_MessageFlags.XL_CAN_MSG_FLAG_REMOTE_FRAME)
                                == XLDefine.XL_MessageFlags.XL_CAN_MSG_FLAG_REMOTE_FRAME)
                            {
                                continue;
                            }
                            else
                            {
                                String str = "Read:";
                                UInt32 ID = (UInt32)receivedEvents[i].tagData.can_Msg.id;
                                str += "Frame Type:";
                                if ((ID & 0x80000000) == 0)
                                    str += "STD ";
                                else
                                    str += "EXT ";
                                str += "Data;";

                                str += "Frame ID:0x" + System.Convert.ToString((ID & 0x7FFFFFFF), 16);

                                //////////////////////////////////////////
                                str += " Data:";
                                byte len = (byte)(receivedEvents[i].tagData.can_Msg.dlc % 9);
                                byte j = 0;
                                if (j++ < len)
                                    str += " " + System.Convert.ToString(receivedEvents[i].tagData.can_Msg.data[0], 16);
                                if (j++ < len)
                                    str += " " + System.Convert.ToString(receivedEvents[i].tagData.can_Msg.data[1], 16);
                                if (j++ < len)
                                    str += " " + System.Convert.ToString(receivedEvents[i].tagData.can_Msg.data[2], 16);
                                if (j++ < len)
                                    str += " " + System.Convert.ToString(receivedEvents[i].tagData.can_Msg.data[3], 16);
                                if (j++ < len)
                                    str += " " + System.Convert.ToString(receivedEvents[i].tagData.can_Msg.data[4], 16);
                                if (j++ < len)
                                    str += " " + System.Convert.ToString(receivedEvents[i].tagData.can_Msg.data[5], 16);
                                if (j++ < len)
                                    str += " " + System.Convert.ToString(receivedEvents[i].tagData.can_Msg.data[6], 16);
                                if (j++ < len)
                                    str += " " + System.Convert.ToString(receivedEvents[i].tagData.can_Msg.data[7], 16);

                                if (Stop_Data_Show_Flag == 0)
                                {
                                    if (this.InvokeRequired)
                                    {
                                        this.Invoke(new CanSendDataShow(ShowSendData), new object[] { str });
                                    }
                                    else
                                    {
                                        ShowSendData(str);
                                    }
                                }
                                if ((ID & 0x7FF) != CAN_UDS_PHYS_RESP)
                                {
                                    continue;
                                }
                                for (uint jj = 0; jj < 8; jj++)
                                {
                                    ReadData[jj] = receivedEvents[i].tagData.can_Msg.data[jj];
                                }
                                CANData_Run((ID & 0x7FFFFFFF), ReadData);

                            }
                        }
                    }
                }           
            }
        
        }

        private void Main_Load(object sender, EventArgs e)
        {
            CanCloseButton.Enabled = false;
            try
            {
                string LogPath = Directory.GetCurrentDirectory();
                string LogPath_txt = LogPath + "\\LogData.txt";
                if (!File.Exists(LogPath_txt))
                {
                  
                }
                else
                {
                    FileStream NewLogFile = new FileStream(LogPath_txt, FileMode.Open, FileAccess.ReadWrite);
                    StreamReader ReadFile = new StreamReader(NewLogFile);
                  
                    Save_Hex_Pacth = ReadFile.ReadLine();

                    ReadFile.Close();
                    NewLogFile.Close();
                }
            }
            catch
            {

            }
        }

        private void MainClosed(object sender, FormClosedEventArgs e)
        {
            if (Main_GlobalData.CAN_Open_Flag == 1)
            {
                if (Main.Main_GlobalData.Dev_Type != 0)
                {
                    VCI_CloseDevice(Main.Main_GlobalData.Dev_Type, Main.Main_GlobalData.Dev_Index);
                }
                else
                {
                    XLDefine.XL_Status status;

                    status = Main.CANDemo.XL_ClosePort(portHandle);

                    status = Main.CANDemo.XL_CloseDriver();
                }

            }
            Application.Exit();
        }

        #region  ButtonClick事件

        private void CanSetButton_Click(object sender, EventArgs e)
        {
            CANset canOPenFrm = new CANset();
            canOPenFrm.Show();
        }

        private void CanStarButton_Click(object sender, EventArgs e)
        {
            if (Main_GlobalData.CAN_Set_Flag == 0)
            {
                MessageBox.Show("Please SetCAN parameter!", "Error！",MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Main.Main_GlobalData.Dev_Type != 0)
            {
                VCI_StartCAN(Main.Main_GlobalData.Dev_Type, Main.Main_GlobalData.Dev_Index, Main.Main_GlobalData.CAN_Index);
            }
            else
            {
               
            }
            Main_GlobalData.CAN_Open_Flag = 1;
            CanCloseButton.Enabled = true;
            CanStarButton.Enabled = false;
            timer_rec.Enabled = true;
            if (Main.Main_GlobalData.Dev_Type != 0)
            {
                VCI_ClearBuffer(Main.Main_GlobalData.Dev_Type, Main.Main_GlobalData.Dev_Index
                               , Main.Main_GlobalData.CAN_Index);
            }

        }

        private void CanCloseButton_Click(object sender, EventArgs e)
        {
            if (Main_GlobalData.CAN_Open_Flag == 0)
            {
                CanCloseButton.Enabled = false;
                return;
            }
            if (Main.Main_GlobalData.Dev_Type != 0)
            {
                VCI_ResetCAN(Main.Main_GlobalData.Dev_Type, Main.Main_GlobalData.Dev_Index, Main.Main_GlobalData.CAN_Index);
                VCI_CloseDevice(Main.Main_GlobalData.Dev_Type, Main.Main_GlobalData.Dev_Index);
            }
            else
            {
                XLDefine.XL_Status status;

                status = Main.CANDemo.XL_ClosePort(portHandle);

                status = Main.CANDemo.XL_CloseDriver();
            }
            Main_GlobalData.CAN_Open_Flag = 0;
            Main_GlobalData.CAN_Set_Flag = 0;
            CanCloseButton.Enabled = false;
            CanStarButton.Enabled = true;
            timer_rec.Enabled = false;
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            fileNameText_Click(sender, e);
        }

        private void SaveMessButton_Click(object sender, EventArgs e)
        {
            Stream TxT_File;
            saveFileDialog1.Filter = "TXT files (*.txt)|*.txt|All files (*.*)|*.*"; //过滤文件类型
            saveFileDialog1.InitialDirectory = "E:\\"; // Directory.GetCurrentDirectory(); //设定初始目录
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((TxT_File = saveFileDialog1.OpenFile()) != null)
                {
                    StreamWriter sw = new StreamWriter(TxT_File, Encoding.GetEncoding("gb2312"));
                    for (int i = 0; i < lb_IAPinfo.Items.Count; i++)
                    {
                        sw.WriteLine(lb_IAPinfo.Items[i].ToString());
                    }
                    sw.Close();
                    TxT_File.Close();
                }
            }
        }

        private void CleanDataButton_Click(object sender, EventArgs e)
        {
            listBox_Info.Items.Clear();
            lb_IAPinfo.Items.Clear();
        }

        private void StopShowButton_Click(object sender, EventArgs e)
        {
            if (Stop_Data_Show_Flag == 0)
            {
                Stop_Data_Show_Flag = 1;
                StopShowButton.Text = "Open Data Display";
            }
            else
            {
                Stop_Data_Show_Flag = 0;
                StopShowButton.Text = "Stop Data Display";
            }

        }

        private void fileNameText_Click(object sender, EventArgs e)
        {
            OpenFileDialog hexfile = new OpenFileDialog();
            hexfile.Filter = "hex files (*.hex)|*.hex|All files (*.*)|*.*"; //过滤文件类型
            hexfile.InitialDirectory = Save_Hex_Pacth;//设定初始目录
            //hexfile.ShowReadOnly = true; //设定文件是否只读
            hexfile.AddExtension = false;

            if (hexfile.ShowDialog() == DialogResult.OK)
            {
                hexFileName = hexfile.FileName;
                fileNameText.Text = hexFileName;
                //保存路径
                string type = hexFileName.Substring(hexFileName.LastIndexOf("\\"));
                Save_Hex_Pacth = hexFileName.Remove(hexFileName.Length - type.Length, type.Length);
                string LogPath = Directory.GetCurrentDirectory();
                string LogPath_txt = LogPath + "\\LogData.txt";
                if (!File.Exists(LogPath_txt))
                {
                    FileStream NewLogFile = new FileStream(LogPath_txt, FileMode.Create, FileAccess.ReadWrite);
                    StreamWriter WriteFile = new StreamWriter(NewLogFile);

                    WriteFile.WriteLine(Save_Hex_Pacth);
                    WriteFile.Close();
                    NewLogFile.Close();
                }
                else
                {
                    FileStream NewLogFile = new FileStream(LogPath_txt, FileMode.Open, FileAccess.ReadWrite);
                    StreamWriter WriteFile = new StreamWriter(NewLogFile);

                    WriteFile.WriteLine(Save_Hex_Pacth);

                    WriteFile.Close();
                    NewLogFile.Close();
                }

                //StreamReader srTxt = new StreamReader(hexFileName, Encoding.UTF8);        //read hex file  to textbox
                //textBox1.Text = srTxt.ReadToEnd();
                //srTxt.Close();  //读取文件并关闭
                int pos = -1;
                if (!(hexFileName.IndexOf(@"\") == -1))
                {
                    pos = hexFileName.LastIndexOf(@".");
                }
                string s = hexFileName.Substring(pos + 1);
                if (s == "hex")
                {
                    lb_IAPinfo.Items.Add(DateTime.Now.ToString("HH:mm:ss ") + "Load HEX IAP File Ok！");
                }
                else
                {

                    label_Len.Text = "00000";
                    label_Line.Text = "0000";
                    label_Version.Text = "-----";
                    label_PrjVer.Text = "----";
                    return;
                }
                FileInfo hexInfo = new FileInfo(hexFileName);
                label_Len.Text = hexInfo.Length.ToString();
                string[] hexReadLines = File.ReadAllLines(hexFileName, Encoding.Default);
                label_Line.Text = hexReadLines.Length.ToString();
                if (hexReadLines[0].Substring(0, 1) != "#")
                {
                    label_Version.Text = "NO version informaton ";
                    label_Node.Text = "-----";
                    label_PrjVer.Text = "----";
                }
                else if ((hexReadLines[0].Length != 17) || (hexReadLines[0].Substring(16, 1) != "#"))
                {
                    label_Version.Text = "Version informaton error ";
                    label_Node.Text = "-----";
                    label_PrjVer.Text = "----";
                }
                else
                {
                    label_Version.Text = hexReadLines[0];
                    label_Node.Text = hexReadLines[0].Substring(3, 3);
                    label_PrjVer.Text = hexReadLines[0].Substring(10, 3);
                }          
            }
        }
     
        private void checkBox_Broadcast_CheckedChanged(object sender, EventArgs e)
        {
         
        }

        private void checkBox_Single_CheckedChanged(object sender, EventArgs e)
        {
           
        }
        #endregion

        #region  委托，功能

        /// <summary>
        /// 委托函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        private delegate void CanSendDataShow(string strData);     // 委托
        public void ShowSendData(string strData)
        {
            listBox_Info.Items.Add(strData);
            listBox_Info.SelectedIndex = listBox_Info.Items.Count - 1;
        }

        private delegate void DeleInforShow(string strData);     // 委托
        public void IAP_InforShow(string strData)
        {
            lb_IAPinfo.Items.Add(strData);
            lb_IAPinfo.SelectedIndex = lb_IAPinfo.Items.Count - 1;
        }

        private delegate void HexTotalPageShow(UInt32 pageN);     // 委托
        public void TotalPageShow(UInt32 pageN)
        {
            label_totalPage.Text = pageN.ToString("d3");
            //tool_MN.Text = pageN.ToString("d3");
        }

        private delegate void DeleCurPageN(string strData);     // 委托
        public void CurPageN(string strData)
        {
            lb_curPage.Text = "No." + strData;
        }

        private delegate void DeleBtnChange();     // 委托
        public void BtnChange()
        {
            btnStartIAP.Text = "Start IAPDownload";
            btnStartIAP.ImageIndex = 0;
        }

        private delegate void DeleEnableSet();     // 委托
        public void EnableSet()
        {
           
        }

        private delegate void SfteVerInforShow(string strData);     // 委托
        public void IAP_SoftverShow(string strData)
        {
           
        }

        /// <summary>
        /// IAP功能函数 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        unsafe public void SendDataReplace(byte []data)
        {
            if (Main.Main_GlobalData.Dev_Type != 0)
            {
                fixed (VCI_CAN_OBJ* sendobjs = &sendobj[0])
                {
                    sendobjs[0].Data[0] = data[0];
                }
                fixed (VCI_CAN_OBJ* sendobjs = &sendobj[0])
                {
                    sendobjs[0].Data[1] = data[1];
                }
                fixed (VCI_CAN_OBJ* sendobjs = &sendobj[0])
                {
                    sendobjs[0].Data[2] = data[2];
                }
                fixed (VCI_CAN_OBJ* sendobjs = &sendobj[0])
                {
                    sendobjs[0].Data[3] = data[3];
                }
                fixed (VCI_CAN_OBJ* sendobjs = &sendobj[0])
                {
                    sendobjs[0].Data[4] = data[4];
                }
                fixed (VCI_CAN_OBJ* sendobjs = &sendobj[0])
                {
                    sendobjs[0].Data[5] = data[5];
                }
                fixed (VCI_CAN_OBJ* sendobjs = &sendobj[0])
                {
                    sendobjs[0].Data[6] = data[6];
                }
                fixed (VCI_CAN_OBJ* sendobjs = &sendobj[0])
                {
                    sendobjs[0].Data[7] = data[7];
                }
            }
            else
            {
                if (sendobj[0].ExternFlag == 0)
                {
                    xlEventCollection.xlEvent[0].tagData.can_Msg.id = sendobj[0].ID;
                }
                else
                {
                    xlEventCollection.xlEvent[0].tagData.can_Msg.id = (sendobj[0].ID | 0x80000000);
                }
                
                xlEventCollection.xlEvent[0].tagData.can_Msg.dlc = sendobj[0].DataLen;
                xlEventCollection.xlEvent[0].tagData.can_Msg.data[0] = data[0];
                xlEventCollection.xlEvent[0].tagData.can_Msg.data[1] = data[1];
                xlEventCollection.xlEvent[0].tagData.can_Msg.data[2] = data[2];
                xlEventCollection.xlEvent[0].tagData.can_Msg.data[3] = data[3];
                xlEventCollection.xlEvent[0].tagData.can_Msg.data[4] = data[4];
                xlEventCollection.xlEvent[0].tagData.can_Msg.data[5] = data[5];
                xlEventCollection.xlEvent[0].tagData.can_Msg.data[6] = data[6];
                xlEventCollection.xlEvent[0].tagData.can_Msg.data[7] = data[7];
                xlEventCollection.xlEvent[0].tag = XLDefine.XL_EventTags.XL_TRANSMIT_MSG;
            }
        }       
        public void AddToDispInfo(string strData)
        {
            strData = DateTime.Now.ToString("HH:mm:ss ") + strData;
            if (this.InvokeRequired)
            {
                this.Invoke(new DeleInforShow(IAP_InforShow), new object[] { strData });
            }
            else
            {
                IAP_InforShow(strData);
            }
        }
        public void AddTotalPageN(UInt32 pageN)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new HexTotalPageShow(TotalPageShow), new object[] { pageN });
            }
            else
            {
                TotalPageShow(pageN);
            }
        }
        public void AddSendPageN(UInt32 pageN)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DeleCurPageN(CurPageN), new object[] { pageN.ToString() });
            }
            else
            {
                CurPageN(pageN.ToString("d3"));
            }
        }
        public void SendCanShow()
        {
            string strdata = "";
            if (Main.Main_GlobalData.Dev_Type != 0)
            {
                strdata = "Send :";
                strdata += "Frame Type:";
                if (sendobj[0].ExternFlag == 0)
                    strdata += "STD ";
                else
                    strdata += "EXT ";
                if (sendobj[0].RemoteFlag == 0)
                    strdata += "Data;";
                else
                    strdata += "Remote;";

                strdata += "Frame ID:0x" + System.Convert.ToString((Int32)sendobj[0].ID, 16);


                //////////////////////////////////////////
                if (sendobj[0].RemoteFlag == 0)
                {
                    strdata += " Data:";
                    byte len = (byte)(sendobj[0].DataLen % 9);
                    byte j = 0;
                    if (j++ < len)
                        strdata += " " + System.Convert.ToString(SendData[0], 16);
                    if (j++ < len)
                        strdata += " " + System.Convert.ToString(SendData[1], 16);
                    if (j++ < len)
                        strdata += " " + System.Convert.ToString(SendData[2], 16);
                    if (j++ < len)
                        strdata += " " + System.Convert.ToString(SendData[3], 16);
                    if (j++ < len)
                        strdata += " " + System.Convert.ToString(SendData[4], 16);
                    if (j++ < len)
                        strdata += " " + System.Convert.ToString(SendData[5], 16);
                    if (j++ < len)
                        strdata += " " + System.Convert.ToString(SendData[6], 16);
                    if (j++ < len)
                        strdata += " " + System.Convert.ToString(SendData[7], 16);

                }
            }
            else
            {
                UInt32 ID = (UInt32)xlEventCollection.xlEvent[0].tagData.can_Msg.id;
                strdata += "Frame Type:";
                if ((ID & 0x80000000) == 0)
                    strdata += "STD ";
                else
                    strdata += "EXT ";
                strdata += "Data;";

                strdata += "Frame ID:0x" + System.Convert.ToString((ID & 0x7FFFFFFF), 16);

                //////////////////////////////////////////
                strdata += " Data:";
                byte len = (byte)(xlEventCollection.xlEvent[0].tagData.can_Msg.dlc % 9);
                byte j = 0;
                if (j++ < len)
                    strdata += " " + System.Convert.ToString(xlEventCollection.xlEvent[0].tagData.can_Msg.data[0], 16);
                if (j++ < len)
                    strdata += " " + System.Convert.ToString(xlEventCollection.xlEvent[0].tagData.can_Msg.data[1], 16);
                if (j++ < len)
                    strdata += " " + System.Convert.ToString(xlEventCollection.xlEvent[0].tagData.can_Msg.data[2], 16);
                if (j++ < len)
                    strdata += " " + System.Convert.ToString(xlEventCollection.xlEvent[0].tagData.can_Msg.data[3], 16);
                if (j++ < len)
                    strdata += " " + System.Convert.ToString(xlEventCollection.xlEvent[0].tagData.can_Msg.data[4], 16);
                if (j++ < len)
                    strdata += " " + System.Convert.ToString(xlEventCollection.xlEvent[0].tagData.can_Msg.data[5], 16);
                if (j++ < len)
                    strdata += " " + System.Convert.ToString(xlEventCollection.xlEvent[0].tagData.can_Msg.data[6], 16);
                if (j++ < len)
                    strdata += " " + System.Convert.ToString(xlEventCollection.xlEvent[0].tagData.can_Msg.data[7], 16);
            }
            if (this.InvokeRequired)
            {
                this.Invoke(new CanSendDataShow(ShowSendData), new object[] { strdata });
            }
            else
            {
                ShowSendData(strdata);
            }
        }

        //根据数据页整理Send 每页数据
        public byte IAP_Send_8_Data(int index ) 
        {
            byte i;
            for (i = 0; i < 8; i++)
            {
                SendData[i] = 0x55;
            }
            
                
            SendData[0] =(Byte)( 0x20 | (index  & 0x0F));
            for (i = 1; i < 8; i++)
            {
                if (IAP_P2BSendDataIndex < IAP_P2BPageData_Length)
                {
                    SendData[i] = (byte)(IAP_P2BSendDataAry[IAP_P2BSendDataIndex++]);
                }
                
            }
            SendDataReplace(SendData);
            
            return 1;
           
        }
        // 读页、整理数据、返回校检值
        public UInt16 DecodeSendData(uint hex_lineindex)
        {
            string tmpStr;
            UInt32 tmpLength;
            UInt32 lineIndex;
            UInt16 Return_DataLen = 0;

            for (lineIndex = 0; lineIndex < 65535; lineIndex++)  //清空
            {
                IAP_P2BSendDataAry[lineIndex] = 0xFF;
            }
            for (lineIndex = hex_lineindex; lineIndex < HEX_EndIndex[IAP_PageIndex]; lineIndex++ , IAP_P2BLine_index++)
            {
                tmpStr = Hex_FileLinesBuff[lineIndex].Substring(7, 2);   // 获取代码

                if (tmpStr == "00")   // 数据地址
                {                  
                    tmpStr = "0x" + Hex_FileLinesBuff[lineIndex].Substring(1, 2);  // 获取长度
                    tmpLength = (UInt32)Convert.ToInt32(tmpStr, 16);   // 长度
                   
                    for (UInt32 j = 0; j < tmpLength; j++)
                    {
                        tmpStr = "0x" + Hex_FileLinesBuff[lineIndex].Substring((int)(9 + j * 2), 2);
                        IAP_P2BSendDataAry[j + Return_DataLen] = (byte)Convert.ToInt32(tmpStr, 16);
                    }
                    Return_DataLen += (UInt16)tmpLength;

                    if (Return_DataLen >= IAP_B2PData_Length - 2)
                    {
                        IAP_P2BLine_index++;
                        break;
                    }        
                }
                else
                {
                }
            }
            return Return_DataLen;
        }        
        //CAN Send 函数调用函数
        public void CANData_Send()
        {
            try
            {
                if (Main.Main_GlobalData.Dev_Type != 0)
                {
                    if (VCI_Transmit(Main_GlobalData.Dev_Type, Main_GlobalData.Dev_Index, Main_GlobalData.CAN_Index, ref sendobj[0], 1) == 0)
                    {
                        AddToDispInfo("Session Convert Commond Send failure，Send again");
                    }
                }
                else
                {
                    if (CANDemo.XL_CanTransmit(portHandle, txMask, xlEventCollection) != XLDefine.XL_Status.XL_SUCCESS)
                    {
                        AddToDispInfo("Session Convert Commond Send failure，Send again");
                    }
                }
            }
            catch
            {
                AddToDispInfo("Send Error!!!!!!!!!!!!!!!!");
            }
           
        }

        public void CANData_Run(uint ID, byte[] data)
        {
            #region UDS下载接收流程
            byte ReMsg_Type = (byte)((data[0] & 0xF0) >> 4);
            CAN_UDS_NEG_Flag = 0;

            if (ReMsg_Type == 1)
            {
                CAN_UDS_NEG_Flag = 0;

                CAN_UDS_ReadDataLen = (UInt16)(((UInt16)(data[0] & 0x0F) << 4) + data[1]);
                IAP_B2PNodeSoftVer = System.Text.Encoding.ASCII.GetString(data, 5, 3);
                CAN_UDS_ReadDataSum = 3;
                return;
            }
            else if (ReMsg_Type == 2)
            {
                if (CAN_UDS_ReadDataSum == 3 && (data[0] & 0x0F) == 1)
                {
                    IAP_B2PNodeSoftVer += System.Text.Encoding.ASCII.GetString(data, 1, 7);
                    CAN_UDS_ReadDataSum += 7;
                    IAP_B2PNodeAddrCnt++;
                }
                else if (CAN_UDS_ReadDataSum == 10 && (data[0] & 0x0F) == 2)
                {
                    IAP_B2PNodeSoftVer += System.Text.Encoding.ASCII.GetString(data, 1, 5);
                    CAN_UDS_ReadDataSum += 5;
                }
                else
                {
                    if (CAN_UDS_ReadDataSum == 10 && (data[0] & 0x0F) == 1)
                    {
                        IAP_B2PNodeAddrCnt++;
                    }
                }
                return;
            }
            
            switch (IAP_Status)
            {
                case 0:
                    if (data[1] == UDS_Pos_Resp_Code_t.UDS_POS_RESP__ECU_RESET )
                    {                      
                        CAN_UDS_RESP_State = 1;
                    }
                    break;
                case 1:
                    if (data[1] == UDS_Pos_Resp_Code_t.UDS_POS_RESP__DIAG_SESS_CTRL 
                        && data[2] == UDS_Diag_Sess_Type_t.UDS_DIAG_SESS_TYPE__EXTD_DIAG_SESS )
                    {                      
                        CAN_UDS_RESP_State = 1;
                    }
                    else
                    {
                        CAN_UDS_RESP_State = 0;
                        CAN_UDS_NEG_Flag = data[3];
                    }

                    break;
                case 2:
                    if (ReMsg_Type == 3)
                    {
                        CAN_UDS_RESP_State = 1;
                    }
                    else
                    {
                        CAN_UDS_RESP_State = 0;
                    }
                    break;
                case 3:
                    if (data[1] == UDS_Pos_Resp_Code_t.UDS_POS_RESP__ROUTINE_CTRL)
                    {                     
                        CAN_UDS_RESP_State = 1;
                    }
                    else
                    {
                        CAN_UDS_RESP_State = 0;                      
                    }
                    break;
                case 4:
                    if (ReMsg_Type == 3)
                    {
                        CAN_UDS_RESP_State = 1;
                    }
                    else
                    {
                        CAN_UDS_RESP_State = 0;
                    }

                    break;
                case 5:
                    if (data[1] == UDS_Pos_Resp_Code_t.UDS_POS_RESP__RQST_DOWNLOAD)
                    {                     
                        CAN_UDS_RESP_State = 1;

                        IAP_B2PData_Length = (UInt16)(data[3] * 256 + data[4]);
                    }
                    else
                    {
                        CAN_UDS_RESP_State = 0;                      
                    }
                  
                    break;
                case 6:
                    if (ReMsg_Type == 3)
                    {
                        CAN_UDS_RESP_State = 1;
                    }
                    else
                    {
                        CAN_UDS_RESP_State = 0;
                    }

                    
                    break;
                case 7:
                    byte sendindex2 = IAP_P2BSendIndex;
                    
                    if (data[1] == UDS_Pos_Resp_Code_t.UDS_POS_RESP__TRANSFER_DATA
                        && data[2] == sendindex2)
                    {                
                        CAN_UDS_RESP_State = 1;
                    }
                    else if (data[1] == 0x7F)
                    {
                        CAN_UDS_RESP_State = 0;
                        CAN_UDS_NEG_Flag = data[3];                        
                    }

                    break;
                case 9:
                    if (data[1] == UDS_Pos_Resp_Code_t.UDS_POS_RESP__RQST_TRANSFER_EXIT)
                    {                     
                        CAN_UDS_RESP_State = 1;
                    }
                    else
                    {
                        CAN_UDS_RESP_State = 0;                      
                    }
                    break;
                case 10:
                  
                    break;
                case 11:
                    if (data[1] == UDS_Pos_Resp_Code_t.UDS_POS_RESP__ROUTINE_CTRL)
                    {                     
                        CAN_UDS_RESP_State = 1;
                    }
                    else
                    {
                        CAN_UDS_RESP_State = 0;                      
                    }
                    break;
                default:
                    break;
            }
            #endregion
        }

        #endregion

        //退出IAPDownload流程
        private void IAP_ThreadExit()
        {
            sendobj.Initialize();
            sendobj[0].SendType = 0;  //0 正常Send   1单次正常Send 
            sendobj[0].RemoteFlag = 0; // 数据帧
            sendobj[0].ExternFlag = CAN_DATA_TYPE; // 扩展帧
            sendobj[0].DataLen = 8;
            for (uint i = 0; i < 8; i++)
            {
                SendData[i] = 0x55;
            }
            sendobj[0].ID = CAN_UDS_SEND_ADDR;
            SendData[0] = 0x02;
            SendData[1] = UDS_Serv_ID_t.UDS_SERV_ID__ECU_RESET;
            SendData[2] = 0x01;
            SendDataReplace(SendData);

            if (Stop_Data_Show_Flag == 0) //StopData Display
            {
                SendCanShow();
            }
            CANData_Send();

            IAP_Status = 0;
            IAP_P2BSendPageIndex = 0;
            if (BT_mode == 0)
            {
                AddToDispInfo("IAPDownload Thread exit ！");
            }
            else
            {
                AddToDispInfo("IAPDownload Single finish，Please Download again！");
            }
            AddTotalPageN(0);
            AddSendPageN(0);
            if (this.InvokeRequired)
            {
                this.Invoke(new DeleEnableSet(EnableSet));
            }
            else
            {
                EnableSet();
            }

            gIAP_startFlag = 0;
            gIAP_checkOk = 0;
            if (this.InvokeRequired)
            {
                this.Invoke(new DeleBtnChange(BtnChange));
            }
            else
            {
                BtnChange();
            }
          
            try
            {
                IAP_Thread.Abort();             
            }
            catch (System.Exception ex)
            {
               
            }
        }
        //进行IAPDownload流程
        public void IAP_Run()
        {
            #region UDS下载流程

            for (int i = 0; i < 1024; i++)
            {
                IAP_P2BSendPageBuf[i] = 0;
            }
            try
            {
                //整理页编号
                IAP_P2BSendPageSum = 0;
                IAP_P2BSendPageIndex = 0;
                IAP_P2BDataByteSum = 0;
                IAP_P2BSendIndex = 0;
                IAP_P2BLine_index = 0;
                IAP_P2BSendTimerCnt = 0;                
                CAN_UDS_POS_RESP_Flag = 0;

                IAP_PageIndex = 0;
                // Set帧格式、类型
                sendobj.Initialize();
                sendobj[0].SendType = 0;  //0 正常Send   1单次正常Send 
                sendobj[0].RemoteFlag = 0; // 数据帧
                sendobj[0].ExternFlag = CAN_DATA_TYPE; // 扩展帧
                sendobj[0].DataLen = 8;
                IAP_Status = 0;
                AddToDispInfo("Send Start IAPDownload Command...");
                while (true)
                {
                    switch (IAP_Status)
                    {
                        case 0:
                            AddToDispInfo("Send Reset Command...");
                            CAN_UDS_RESP_State = 0;
                            for (uint i = 0; i < 8; i++)
                            {
                                SendData[i] = 0x55;
                            }
                            sendobj[0].ID = CAN_UDS_SEND_ADDR;
                            SendData[0] = 0x02;
                            SendData[1] = UDS_Serv_ID_t.UDS_SERV_ID__ECU_RESET;
                            SendData[2] = 0x60;
                            SendDataReplace(SendData);

                            if (Stop_Data_Show_Flag == 0) //StopData Display
                            {
                                SendCanShow();
                            }

                            CANData_Send();

                            Thread.Sleep(100);
                            IAP_Status = 1;
                            break;
                        case 1:
                            AddToDispInfo("Send Session Convert Command...");
                            CAN_UDS_RESP_State = 0;
                            for (uint i = 0; i < 8; i++)
                            {
                                SendData[i] = 0x55;
                            }
                            sendobj[0].ID = CAN_UDS_SEND_ADDR;
                            SendData[0] = 0x02;
                            SendData[1] = UDS_Serv_ID_t.UDS_SERV_ID__DIAG_SESS_CTRL;
                            SendData[2] = UDS_Diag_Sess_Type_t.UDS_DIAG_SESS_TYPE__PGM_SESS;                         
                            SendDataReplace(SendData);

                            if (Stop_Data_Show_Flag == 0) //StopData Display
                            {
                                SendCanShow();                             
                            }
                            CANData_Send();

                            Thread.Sleep(500);

                           
                            AddToDispInfo("waiting for conversion to complete..");
                            while (CAN_UDS_RESP_State == 0)
                            {
                                Thread.Sleep(100);
                                IAP_P2BSendTimerCnt++;
                                if (IAP_P2BSendTimerCnt > 5)
                                {
                                    IAP_P2BSendTimerCnt = 0;
                                    AddToDispInfo("Sess conveet No  reply ..");
                                    IAP_Status = 2;
                                    break;
                                }
                            }

                            if (CAN_UDS_RESP_State == 1)
                            {
                                IAP_Status = 2;
                            }
                            
                            break;
                        case 2:
                            AddToDispInfo("Send ROUTINE_CTRL Command。");
                            CAN_UDS_RESP_State = 0;

                            for (uint i = 0; i < 8; i++)
                            {
                                SendData[i] = 0x55;
                            }
                            sendobj[0].ID = CAN_UDS_SEND_ADDR;
                            SendData[0] = 0x10;
                            SendData[1] = 0x0C;
                            SendData[2] = UDS_Serv_ID_t.UDS_SERV_ID__ROUTINE_CTRL;
                            SendData[3] = 0x01;
                            SendData[4] = 0xFF;
                            SendData[5] = 0x00;
                            SendData[6] = (byte)(HEX_StarAddress[IAP_PageIndex] >> 24);
                            SendData[7] = (byte)(HEX_StarAddress[IAP_PageIndex] >> 16);
                            SendDataReplace(SendData);

                            if (Stop_Data_Show_Flag == 0) //StopData Display
                            {
                                SendCanShow();
                            }
                            CANData_Send();

                            Thread.Sleep(10);
                            while (CAN_UDS_RESP_State == 0)
                            {
                                IAP_P2BSendTimerCnt++;
                                if (IAP_P2BSendTimerCnt > 5)
                                {
                                    AddToDispInfo("Control Frame NO reply，Download exit");
                                    IAP_ThreadExit();
                                    break;
                                }
                                Thread.Sleep(100);
                            }

                            IAP_P2BSendTimerCnt = 0;
                            IAP_Status = 3;                         

                            break;
                        case 3:
                            CAN_UDS_RESP_State = 0;
                            for (uint i = 0; i < 8; i++)
                            {
                                SendData[i] = 0x55;
                            }
                            sendobj[0].ID = CAN_UDS_SEND_ADDR;
                            SendData[0] = 0x21;
                            SendData[1] = (byte)(HEX_StarAddress[IAP_PageIndex] >> 8);
                            SendData[2] = (byte)(HEX_StarAddress[IAP_PageIndex] >> 0);
                            SendData[3] = (byte)(HEX_PageSize[IAP_PageIndex] >> 24);
                            SendData[4] = (byte)(HEX_PageSize[IAP_PageIndex] >> 16);
                            SendData[5] = (byte)(HEX_PageSize[IAP_PageIndex] >> 8);
                            SendData[6] = (byte)(HEX_PageSize[IAP_PageIndex] >> 0);
                            SendDataReplace(SendData);

                            if (Stop_Data_Show_Flag == 0) //StopData Display
                            {
                                SendCanShow();
                            }

                            CANData_Send();

                            Thread.Sleep(100);

                            while (CAN_UDS_RESP_State == 0)
                            {
                                Thread.Sleep(200);
                                AddToDispInfo("Waitting Positive reply..");
                                IAP_P2BSendTimerCnt++;
                                if (IAP_P2BSendTimerCnt > 5)
                                {
                                    AddToDispInfo("request Download NO reply，Download exit");
                                    IAP_ThreadExit();
                                    break;
                                }
                            }
                            if (CAN_UDS_RESP_State == 1)
                            {
                                IAP_Status = 4;
                                AddToDispInfo("Start request Download 。。。");
                                IAP_P2BSendTimerCnt = 0;
                            }

                            break;                       
                        case 4:
                            //if (Main.Main_GlobalData.Dev_Type != 0)
                            //{
                            //    VCI_ClearBuffer(Main_GlobalData.Dev_Type, Main_GlobalData.Dev_Index, Main_GlobalData.CAN_Index);
                            //}
                            CAN_UDS_RESP_State = 0;
                            AddToDispInfo("Send request Download  Command。");
                            for (uint i = 0; i < 8; i++)
                            {
                                SendData[i] = 0x55;
                            }
                            sendobj[0].ID = CAN_UDS_SEND_ADDR;
                            SendData[0] = 0x10;
                            SendData[1] = 0x0B;
                            SendData[2] = UDS_Serv_ID_t.UDS_SERV_ID__RQST_DOWNLOAD;
                            SendData[3] = 0x00;
                            SendData[4] = 0x44;
                            SendData[5] = (byte)(HEX_StarAddress[IAP_PageIndex] >> 24);
                            SendData[6] = (byte)(HEX_StarAddress[IAP_PageIndex] >> 16);
                            SendData[7] = (byte)(HEX_StarAddress[IAP_PageIndex] >> 8);
                            SendDataReplace(SendData);

                            if (Stop_Data_Show_Flag == 0) //StopData Display
                            {
                                SendCanShow();
                            }
                            CANData_Send();

                            Thread.Sleep(10);
                            while (CAN_UDS_RESP_State == 0)
                            {
                                IAP_P2BSendTimerCnt++;
                                if (IAP_P2BSendTimerCnt > 5)
                                {
                                    AddToDispInfo("Control Frame NO reply，Download exit");
                                    IAP_ThreadExit();
                                    break;
                                }
                                Thread.Sleep(100);
                            }

                            IAP_P2BSendTimerCnt = 0;
                            IAP_Status = 5;

                            break;
                        case 5:
                            //if (Main.Main_GlobalData.Dev_Type != 0)
                            //{
                            //    VCI_ClearBuffer(Main_GlobalData.Dev_Type, Main_GlobalData.Dev_Index, Main_GlobalData.CAN_Index);
                            //}
                            CAN_UDS_RESP_State = 0;
                            for (uint i = 0; i < 8; i++)
                            {
                                SendData[i] = 0x55;
                            }
                            sendobj[0].ID = CAN_UDS_SEND_ADDR;
                            SendData[0] = 0x21;
                            SendData[1] = (byte)(HEX_StarAddress[IAP_PageIndex] >> 0);
                            SendData[2] = (byte)(HEX_TransferSize[IAP_PageIndex] >> 24);
                            SendData[3] = (byte)(HEX_TransferSize[IAP_PageIndex] >> 16);
                            SendData[4] = (byte)(HEX_TransferSize[IAP_PageIndex] >> 8);
                            SendData[5] = (byte)(HEX_TransferSize[IAP_PageIndex] >> 0);   
                            SendDataReplace(SendData);

                            if (Stop_Data_Show_Flag == 0) //StopData Display
                            {
                                SendCanShow();
                            }

                            CANData_Send();
                            Thread.Sleep(100);

                            while (CAN_UDS_RESP_State == 0)
                            {
                                IAP_P2BSendTimerCnt++;
                                if (IAP_P2BSendTimerCnt > 5)
                                {
                                    AddToDispInfo("Request Download Timeout ，Download exit");
                                    IAP_ThreadExit();
                                    break;
                                }
                                Thread.Sleep(100);
                            }

                            if (CAN_UDS_RESP_State == 1)
                            {
                                IAP_Status = 6;
                                AddToDispInfo("Start Send Data。。。");
                                IAP_P2BSendTimerCnt = 0;
                                IAP_P2BLine_index = (UInt16)HEX_StarIndex[IAP_PageIndex];
                                IAP_P2BSendIndex = 1;

                                if (IAP_B2PData_Length < 2 || IAP_B2PData_Length > 0xFFE)
                                {
                                    IAP_B2PData_Length = 0xF02;
                                } 
                                IAP_P2BSendPageSum = (UInt16)(HEX_TransferSize[IAP_PageIndex] / (IAP_B2PData_Length - 2)+ 1);
                                AddTotalPageN(IAP_P2BSendPageSum);
                                IAP_P2BSendPageIndex = 0;
                            }

                            break;
                        case 6:
                            //if (Main.Main_GlobalData.Dev_Type != 0)
                            //{
                            //    VCI_ClearBuffer(Main_GlobalData.Dev_Type, Main_GlobalData.Dev_Index, Main_GlobalData.CAN_Index);
                            //}
                            CAN_UDS_RESP_State = 0;
                            
                            IAP_P2BPageData_Length = DecodeSendData(IAP_P2BLine_index);
                            IAP_P2BSendDataIndex = 0;
                            IAP_P2BSendPageIndex++;
                            AddSendPageN(IAP_P2BSendPageIndex);

                            if (IAP_P2BPageData_Length < (IAP_B2PData_Length - 2))
                            {
                                IAP_B2PData_Length = (UInt16)(IAP_P2BPageData_Length + 2);
                            }
                            for (uint i = 0; i < 8; i++)
                            {
                                SendData[i] = 0x55;
                            }
                            sendobj[0].ID = CAN_UDS_SEND_ADDR;
                            SendData[0] = (byte)(0x10 + (IAP_B2PData_Length >> 8));
                            SendData[1] = (byte)(IAP_B2PData_Length & 0xFF );
                            SendData[2] = UDS_Serv_ID_t.UDS_SERV_ID__TRANSFER_DATA;
                            SendData[3] = (byte)(IAP_P2BSendIndex);                           
                            SendData[4] = (byte)(IAP_P2BSendDataAry[IAP_P2BSendDataIndex++]);
                            SendData[5] = (byte)(IAP_P2BSendDataAry[IAP_P2BSendDataIndex++]);
                            SendData[6] = (byte)(IAP_P2BSendDataAry[IAP_P2BSendDataIndex++]);
                            SendData[7] = (byte)(IAP_P2BSendDataAry[IAP_P2BSendDataIndex++]);
                            SendDataReplace(SendData);
                            

                                                                                                           
                            if (Stop_Data_Show_Flag == 0) //StopData Display
                            {
                                SendCanShow();
                                AddToDispInfo("IAPSend No." + IAP_P2BSendPageIndex.ToString() + " Page！");
                            }
                            CANData_Send();


                            while (CAN_UDS_RESP_State == 0)
                            {
                                IAP_P2BSendTimerCnt++;
                                if (IAP_P2BSendTimerCnt > 5)
                                {
                                    AddToDispInfo("Data Send NO reply，Download exit");
                                    IAP_ThreadExit();
                                }
                                Thread.Sleep(100);
                            }
                            IAP_Status = 7;
                            IAP_P2BSendTimerCnt = 0;
                            break;
                        case 7:
                            //if (Main.Main_GlobalData.Dev_Type != 0)
                            //{
                            //    VCI_ClearBuffer(Main_GlobalData.Dev_Type, Main_GlobalData.Dev_Index, Main_GlobalData.CAN_Index);
                            //}
                            CAN_UDS_RESP_State = 0;
                            sendobj[0].ID = CAN_UDS_SEND_ADDR;                          
                            for (int tmpIdex = 1; tmpIdex <= ((IAP_P2BPageData_Length - 4 + 6 )/ 7); tmpIdex++)
                            {
                                IAP_Send_8_Data(tmpIdex);
                                if (Stop_Data_Show_Flag == 0) //StopData Display
                                {
                                    SendCanShow();
                                 }

                                CANData_Send();
                               
                            }

                            while (CAN_UDS_RESP_State == 0)
                            {
                                Thread.Sleep(5);
                                IAP_P2BSendTimerCnt++;
                                if (IAP_P2BSendTimerCnt > 500)
                                {
                                    AddToDispInfo(" Page: " + Convert.ToString(IAP_P2BSendPageIndex) + " NO reply。");
                                    AddToDispInfo("Data Send NO reply，Download exit");
                                    IAP_ThreadExit();
                                    break;
                                }
                                if (CAN_UDS_NEG_Flag != 0)
                                {
                                    AddToDispInfo("Negative reply Code:0x" + Convert.ToString(CAN_UDS_NEG_Flag, 16) + " Page: " + Convert.ToString(IAP_P2BSendPageIndex));
                                    IAP_ThreadExit();
                                    break;
                                }
                            }

                            if (IAP_P2BLine_index < HEX_EndIndex[IAP_PageIndex])
                            {
                                IAP_P2BSendIndex++;
                                IAP_Status = 6;
                                IAP_P2BSendTimerCnt = 0;
                            }
                            else
                            {
                                IAP_Status = 9;
                                AddToDispInfo("Data Send Finish。。。");
                                IAP_P2BSendTimerCnt = 0;
                            }
                            break;

                        case 9:
                            //if (Main.Main_GlobalData.Dev_Type != 0)
                            //{
                            //    VCI_ClearBuffer(Main_GlobalData.Dev_Type, Main_GlobalData.Dev_Index, Main_GlobalData.CAN_Index);
                            //}
                            AddToDispInfo("Send Exit Data Download Command。");
                            for (uint i = 0; i < 8; i++)
                            {
                                SendData[i] = 0x55;
                            }
                            sendobj[0].ID = CAN_UDS_SEND_ADDR;
                            SendData[0] = 0x01;
                            SendData[1] = UDS_Serv_ID_t.UDS_SERV_ID__RQST_TRANSFER_EXIT;                        
                            SendDataReplace(SendData);

                            if (Stop_Data_Show_Flag == 0) //StopData Display
                            {
                                SendCanShow();
                            }
                            CANData_Send();

                            Thread.Sleep(100);

                            IAP_P2BSendTimerCnt++;
                            if (IAP_P2BSendTimerCnt > 5)
                            {
                                AddToDispInfo("Exit Data Download No reply，Download exit");
                                IAP_ThreadExit();
                            }
                            if (CAN_UDS_RESP_State == 1)
                            {
                                IAP_Status = 10;
                            }

                            break;

                        case 10:
                            AddToDispInfo("Page: " + Convert.ToString(IAP_PageIndex + 1)+ " Download Finish。");
                            IAP_PageIndex++;
                            if (IAP_PageIndex >= IAP_PageIndexSum)
                            {
                                IAP_Status = 11;
                            }
                            else
                            {
                                IAP_Status = 2;
                            }                           
                            break;
                        case 11:
                            //if (Main.Main_GlobalData.Dev_Type != 0)
                            //{
                            //    VCI_ClearBuffer(Main_GlobalData.Dev_Type, Main_GlobalData.Dev_Index, Main_GlobalData.CAN_Index);
                            //}
                            AddToDispInfo("Send Reset Command.");
                            for (uint i = 0; i < 8; i++)
                            {
                                SendData[i] = 0x55;
                            }
                            sendobj[0].ID = CAN_UDS_SEND_ADDR;
                            SendData[0] = 0x02;
                            SendData[1] = UDS_Serv_ID_t.UDS_SERV_ID__ECU_RESET;
                            SendData[2] = 0x01;
                            SendDataReplace(SendData);

                            if (Stop_Data_Show_Flag == 0) //StopData Display
                            {
                                SendCanShow();
                            }

                            CANData_Send();

                            IAP_ThreadExit();
                            break;
                        
                       
                        default:
                            break;
                    }
                    Thread.Sleep(1);
                }
            }
            catch (System.Exception ex)
            {
                // MessageBox.Show(Convert.ToString(ex), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            #endregion

        }

        /// <summary>
        /// HEX文件读取和校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IAP_SelfTest_Click(object sender, EventArgs e)
        {
            if (hexFileName != "")
            {
                try
                {
                    using (StreamReader sr = new StreamReader(hexFileName))
                    {
                        String line;
                        String tmpString;
                        byte checkResult = 0;
                        byte tmpNum = 1;
                        gIAP_checkOk = 0;
                        // 判断下载类型
                        // 开始文件校检
                        UInt32 lineNum = 0;

                        string Last_line = "000000000000000000";
                        string PageAddr = "";
                        UInt32 Lastaddr = 0;

                        while ((line = sr.ReadLine()) != null)
                        {
                            lineNum++;
                            tmpNum = 1;

                            // 行为零未处理
                            if (line[0] != ':')
                            {
                                lb_IAPinfo.Items.Add(DateTime.Now.ToString("HH:mm:ss ") + " HEX File Error：No." + lineNum.ToString() + "line lack of ：！");
                                gIAP_checkOk = 0;
                                return;
                            }
                            if (line.Length > 10)
                            {
                                while (tmpNum < line.Length - 1)
                                {
                                    tmpString = line.Substring(tmpNum, 2);
                                    checkResult += (byte)Convert.ToInt32(tmpString, 16);
                                    tmpNum += 2;
                                }
                                if (checkResult != 0)
                                {
                                    lb_IAPinfo.Items.Add(DateTime.Now.ToString("HH:mm:ss ") + "HEX File Error：No." + lineNum.ToString() + "line checksum error！");
                                    gIAP_checkOk = 0;
                                    return;
                                }
                            }
                            else
                            {
                                lb_IAPinfo.Items.Add(DateTime.Now.ToString("HH:mm:ss ") + "HEX File Error：No." + lineNum.ToString() + "line data length error！");
                                gIAP_checkOk = 0;
                                return;
                            }

                            string addstemp = "";
                            UInt32 addutemp = 0;
                            if (line.Substring(7, 2) == "02")   //段地址
                            {
                                addstemp = line.Substring(1, 2);
                                PageAddr = line.Substring(9, (int)(UInt32)Convert.ToInt32(addstemp, 16) * 2);
                            }
                            else if (line.Substring(7, 2) == "04") //扩展线性地址
                            {
                                addstemp = line.Substring(1, 2);
                                PageAddr = line.Substring(9, (int)(UInt32)Convert.ToInt32(addstemp, 16) * 2);
                            }
                            else if (line.Substring(7, 2) == "01") //结束
                            {
                                HEX_EndIndex[1] = lineNum - 1;
                                HEX_TransferSize[1] = Lastaddr + Convert.ToUInt32(Last_line.Substring(1, 2), 16) - HEX_StarAddress[1];
                                break;
                            }
                            else if (line.Substring(7, 2) == "00") //地址
                            {
                                addstemp = line.Substring(3, 4);
                               
                                addutemp = (Convert.ToUInt32(PageAddr, 16) << 16 ) + Convert.ToUInt32(addstemp, 16) ;

                                if (Lastaddr + Convert.ToUInt32(Last_line.Substring(1, 2), 16) != addutemp)
                                {
                                    if (addutemp == HEX_StarAddress[0])
                                    {
                                        HEX_StarIndex[0] = lineNum - 1;
                                    }
                                    if (addutemp == HEX_StarAddress[1])
                                    {
                                        HEX_EndIndex[0] = lineNum - 2;
                                        HEX_StarIndex[1] = lineNum - 1;
                                        HEX_TransferSize[0] = Lastaddr + Convert.ToUInt32(Last_line.Substring(1, 2), 16) - HEX_StarAddress[0];
                                    }

                                }

                                Lastaddr = addutemp;
                                Last_line = line;
                            }
                            else
                            { }
                        }
                        sr.Close();

                        Hex_FileLinesBuff = File.ReadAllLines(hexFileName, Encoding.Default);
                        Hex_FileLineNum = (UInt32)Hex_FileLinesBuff.Length;
                        if (Hex_FileLineNum < 2)
                        {
                            lb_IAPinfo.Items.Add(DateTime.Now.ToString("HH:mm:ss ") + "HEX file self check Error：Line too less！");
                            return;
                        }
                        if (Hex_FileLinesBuff[lineNum - 1] != ":00000001FF" || lineNum > Hex_FileLineNum)
                        {
                            lb_IAPinfo.Items.Add(DateTime.Now.ToString("HH:mm:ss ") + "HEX file self check Error：lack of end flag！");
                            return;
                        }
                        if (Hex_FileLineNum > lineNum)
                        {
                            Hex_FileLineNum = lineNum;
                        }               
                        
                        lb_IAPinfo.Items.Add(DateTime.Now.ToString("HH:mm:ss ") + "File:" + hexFileName.Substring(hexFileName.LastIndexOf("\\")+1));
                        lb_IAPinfo.Items.Add(DateTime.Now.ToString("HH:mm:ss ") + "HEX file self check Finish， OK！");
                        gIAP_checkOk = 1;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(Convert.ToString(ex),"", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please Set HEX File Path！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                gIAP_checkOk = 0;
            }
        }

        private void btnStartIAP_Click(object sender, EventArgs e)
        {
            if (gIAP_startFlag == 1)                                        // 已经Start 下载，则关闭
            {
                if (DialogResult.Yes == MessageBox.Show("Confirm whether Stop IAPDownload？", "",MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    IAP_ThreadExit();   
                }
                return;
            }
            if (gIAP_checkOk == 0)
            {
                MessageBox.Show("IAP File SelfChcek Error or No Selfcheck！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (Main_GlobalData.CAN_Open_Flag == 0)
            {
                MessageBox.Show("Please Start USBCAN！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CAN_UDS_SEND_ADDR = CAN_UDS_FUNC_ADDR;

            gIAP_startFlag = 1;
            IAP_Thread = new Thread(new ThreadStart(IAP_Run));   //Start 运行线程
            IAP_Thread.Start();
            btnStartIAP.Text = "StopIAPDownload";
            btnStartIAP.ImageIndex = 1;
        }

        private void fileNameText_MouseHover(object sender, EventArgs e)
        {
            this.toolTip1.Show(hexFileName, this.fileNameText);
        }

    }
}
