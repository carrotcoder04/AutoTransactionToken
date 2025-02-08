

using AutoTransactionToken.AppController;
using System.Windows.Forms;

namespace AutoTransactionToken
{
    public partial class MainForm : Form
    {
        private Controller Controller => Controller.Instance;
        public List<SmartWallet> Wallets => MyData.Wallets;
        public static MainForm Instance { get; private set; }
        public MainForm()
        {
            Instance = this;
            InitializeComponent();
        }
        public static void MessageBox(string message)
        {
            Instance.Invoke(() => System.Windows.Forms.MessageBox.Show(message));
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.Location = new Point(0, screenHeight - this.Height);
            Controller.Init();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Controller.Open();
        }

        private void StartApp_Click(object sender, EventArgs e)
        {
            Controller.StartApp();
        }
        private void TransactionSmartButton_Click(object sender, EventArgs e)
        {
            Controller.OnSmartTransactionClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Controller.OnDumpXMLClick();
        }

        private void SortWindow_Click(object sender, EventArgs e)
        {
            Controller.OnSortWindowClick();
        }

        private void EnergyDelegate_Click(object sender, EventArgs e)
        {
            Controller.OnEnergyDelegateClick();
        }

        private void TransactionUltimaButton_Click(object sender, EventArgs e)
        {
            Controller.OnTransactionUltimaClick();
        }
        private void Update_Tick(object sender, EventArgs e)
        {
            if (DataView.RowCount < Wallets.Count)
            {
                DataView.RowCount = Wallets.Count;
            }
            foreach (SmartWallet wallet in Wallets)
            {
                DataView.Rows[wallet.Index].Cells[0].Value = wallet;
                if (wallet.SmartState == SmartTransactionState.SmartTransactionSuccess)
                {
                    DataView.Rows[wallet.Index].Cells[0].Style.BackColor = Color.Cyan;
                }
                if (wallet.EnergyState == EnergyTransactionState.EnergySuccess)
                {
                    DataView.Rows[wallet.Index].Cells[0].Style.BackColor = Color.Orange;
                }
                if (wallet.BullState == BullTransactionState.BullSuccess)
                {
                    DataView.Rows[wallet.Index].Cells[0].Style.BackColor = Color.MediumPurple;
                }
                if (wallet.UltimaState == UltimaTransactionState.UltimaSuccess)
                {
                    DataView.Rows[wallet.Index].Cells[0].Style.BackColor = Color.Green;
                }
            }
        }

        private void TransactionUltimaFromMainButton_Click(object sender, EventArgs e)
        {
            Controller.OnTransactionUltimaFromMainClick();
        }

        private void EnergyRecoveryButton_Click(object sender, EventArgs e)
        {
            Controller.OnEnergyRecoveryClick();
        }

        private void DataView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(DataView.RowHeadersDefaultCellStyle.ForeColor))
            {
                string rowIndex = (e.RowIndex + 1).ToString();
                e.Graphics.DrawString(rowIndex,
                                      DataView.DefaultCellStyle.Font,
                                      b,
                                      e.RowBounds.Location.X + 15,
                                      e.RowBounds.Location.Y + 4);
            }
        }

        private void ClearAppDataAndReloginButton_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show(
            "Are you sure you want to clear data?",
            "Confirmation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Controller.OnClearAppDataAndReloginClick();
            }
        }

        private void StopApp_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show(
            "Are you sure you want to stop app?",
            "Confirmation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Controller.OnStopAppClick();
            }
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            Controller.OnRegisterClick();
        }

        private void TransactionBullButton_Click(object sender, EventArgs e)
        {
            Controller.OnTransactionBullClick();
        }

        private void LoginTest_Click(object sender, EventArgs e)
        {
            Task.Run(() => Controller.Login(Controller.clients[0], Controller.Wallets[0]));
        }
    }
}
