using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using vxlapi_NET;

namespace CAN_IAP_ForUDS
{
    public partial class CANset : Form
    {
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_OpenDevice(UInt32 DeviceType, UInt32 DeviceInd, UInt32 Reserved);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_CloseDevice(UInt32 DeviceType, UInt32 DeviceInd);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_InitCAN(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_INIT_CONFIG pInitConfig);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_StartCAN(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);
        [DllImport("controlcan.dll")]
        //static extern UInt32 VCI_SetReference(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, UInt32 RefType, ref byte pData);
        unsafe static extern UInt32 VCI_SetReference(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, UInt32 RefType, byte* pData);

        //usb-e-u 波特率
        static UInt32[] GCanBrTab = new UInt32[3]{
                     0x1C0008,0x060007,0x060003
                };

        public CANset()
        {
            InitializeComponent();
        }

        unsafe private void btn_OpenDev_Click_1(object sender, EventArgs e)
        {          
             if (Main.Main_GlobalData.CAN_Open_Flag == 1)
            {
                MessageBox.Show("Please close CAN Device！", "Error",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {              
                if(this.cb_USBCANtype.SelectedItem == null)
                {
                    MessageBox.Show("Pleace select CAN Device Type", "Error",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    if (this.cb_USBCANtype.SelectedIndex == 0)
                    {
                        Main.Main_GlobalData.Dev_Type = 3;
                    }
                    else if (this.cb_USBCANtype.SelectedIndex == 1)
                    {
                        Main.Main_GlobalData.Dev_Type = 4;
                    }
                    else if (this.cb_USBCANtype.SelectedIndex == 2)
                    {
                        Main.Main_GlobalData.Dev_Type = 20;
                    }
                    else if (this.cb_USBCANtype.SelectedIndex == 3)
                    {
                        Main.Main_GlobalData.Dev_Type = 21;
                    }
                    else if (this.cb_USBCANtype.SelectedIndex == 4)
                    {
                        Main.Main_GlobalData.Dev_Type = 0;
                    }
                    else
                    {
                        Main.Main_GlobalData.Dev_Type = 0;
                    }
                }
                if (Main.Main_GlobalData.Dev_Type != 0)
                {
                    Main.Main_GlobalData.Dev_Index = (UInt32)comboBox_DevIndex.SelectedIndex;
                    Main.Main_GlobalData.CAN_Index = (UInt32)combox_CanNum.SelectedIndex;
                    if (VCI_OpenDevice(Main.Main_GlobalData.Dev_Type, Main.Main_GlobalData.Dev_Index, 0) == 0)
                    {
                        MessageBox.Show("Open Device Error, Please Check the settings.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    //USB-E-U 代码
                    UInt32 baud;
                    baud = GCanBrTab[comboBox_BandRate.SelectedIndex];
                    if (VCI_SetReference(Main.Main_GlobalData.Dev_Type, Main.Main_GlobalData.Dev_Index, Main.Main_GlobalData.CAN_Index, 0, (byte*)&baud) != 1)
                    {

                        MessageBox.Show("Setting Baudrate Error，Open Device Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        VCI_CloseDevice(Main.Main_GlobalData.Dev_Index, Main.Main_GlobalData.CAN_Index);
                        return;
                    }
                    Main.Main_GlobalData.CAN_Set_Flag = 1;
                    VCI_INIT_CONFIG config = new VCI_INIT_CONFIG();
                    config.AccCode = System.Convert.ToUInt32("0x" + textBox_AccCode.Text, 16);
                    config.AccMask = System.Convert.ToUInt32("0x" + textBox_AccMask.Text, 16);

                    if (comboBox_BandRate.SelectedIndex == 0)
                    {
                        config.Timing0 = 0x01;
                        config.Timing1 = 0x1C;
                    }
                    else if (comboBox_BandRate.SelectedIndex == 1)
                    {
                        config.Timing0 = 0x00;
                        config.Timing1 = 0x1C;
                    }
                    else if (comboBox_BandRate.SelectedIndex == 2)
                    {
                        config.Timing0 = 0x00;
                        config.Timing1 = 0x14;
                    }

                    config.Filter = (Byte)comboBox_Filter.SelectedIndex;
                    config.Mode = (Byte)comboBox_Mode.SelectedIndex;
                    VCI_InitCAN(Main.Main_GlobalData.Dev_Type, Main.Main_GlobalData.Dev_Index
                                , Main.Main_GlobalData.CAN_Index, ref config);

                }
                else
                {
                    XLDefine.XL_Status status;

                    status = Main.CANDemo.XL_OpenDriver();

                    if (status != XLDefine.XL_Status.XL_SUCCESS)
                    {
                        MessageBox.Show("Open Device Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    status = Main.CANDemo.XL_GetDriverConfig(ref Main.driverConfig);

                    Main.hwType = XLDefine.XL_HardwareType.XL_HWTYPE_VN1640;
                    Main.hwIndex = (UInt32)comboBox_DevIndex.SelectedIndex;
                    Main.hwChannel = (UInt32)combox_CanNum.SelectedIndex;

                    status = Main.CANDemo.XL_SetApplConfig(Main.appName, 0, Main.hwType, Main.hwIndex, Main.hwChannel, XLDefine.XL_BusTypes.XL_BUS_TYPE_CAN);
                   

                    Main.accessMask = Main.CANDemo.XL_GetChannelMask(Main.hwType, (int)Main.hwIndex, (int)Main.hwChannel);
                    Main.txMask = Main.accessMask;

                    Main.permissionMask = Main.accessMask;
                    // Open port
                    status = Main.CANDemo.XL_OpenPort(ref Main.portHandle, Main.appName, Main.accessMask, ref Main.permissionMask, 256, XLDefine.XL_InterfaceVersion.XL_INTERFACE_VERSION, XLDefine.XL_BusTypes.XL_BUS_TYPE_CAN);
                    if (status != XLDefine.XL_Status.XL_SUCCESS)
                    {
                        MessageBox.Show("XL_OpenPort", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                                 
                    // Check port
                    status = Main.CANDemo.XL_CanRequestChipState(Main.portHandle, Main.accessMask);
                    if (status != XLDefine.XL_Status.XL_SUCCESS)
                    {
                        MessageBox.Show("XL_CanRequestChipState", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (comboBox_BandRate.SelectedIndex == 0)
                    {
                        status = Main.CANDemo.XL_CanSetChannelBitrate(Main.portHandle, Main.accessMask, 250000);
                    }
                    else if (comboBox_BandRate.SelectedIndex == 1)
                    {
                        status = Main.CANDemo.XL_CanSetChannelBitrate(Main.portHandle, Main.accessMask, 500000);
                    }
                    else if (comboBox_BandRate.SelectedIndex == 2)
                    {
                        status = Main.CANDemo.XL_CanSetChannelBitrate(Main.portHandle, Main.accessMask, 1000000);
                    }

                    // Get RX event handle
                    status = Main.CANDemo.XL_SetNotification(Main.portHandle, ref Main.eventHandle, 1);
                    if (status != XLDefine.XL_Status.XL_SUCCESS)
                    {
                        MessageBox.Show("XL_SetNotification", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                  
                    // Activate channel
                    status = Main.CANDemo.XL_ActivateChannel(Main.portHandle, Main.accessMask, XLDefine.XL_BusTypes.XL_BUS_TYPE_CAN, XLDefine.XL_AC_Flags.XL_ACTIVATE_RESET_CLOCK);
                    if (status != XLDefine.XL_Status.XL_SUCCESS)
                    {
                        MessageBox.Show("XL_ActivateChannel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    
                    // Reset time stamp clock
                    status = Main.CANDemo.XL_ResetClock(Main.portHandle);
                    if (status != XLDefine.XL_Status.XL_SUCCESS)
                    {
                        MessageBox.Show("XL_ResetClock", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                   
                    Main.Main_GlobalData.CAN_Set_Flag = 1;
                }
                this.Close();
            }

        }

        private void CANset_Load(object sender, EventArgs e)
        {
            cb_USBCANtype.SelectedIndex = 3;
            comboBox_DevIndex.SelectedIndex = 0;
            combox_CanNum.SelectedIndex = 0;
            comboBox_BandRate.SelectedIndex = 2;
            comboBox_Mode.SelectedIndex = 0;
            comboBox_Filter.SelectedIndex = 0;
        }

    }
}
