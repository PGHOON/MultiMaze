using MazeClient;
using MazeClient.Scenes;


namespace MazeClient
{
    public partial class MainScene : Form
    {
        GameManager Manager;
        public MainScene()
        {
            InitializeComponent();
            //Manager �Ҵ�
            Manager = GameManager.Instance;

            // �ε��̳� ������ �񵿱������� �޾ƾ���. 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Manager.server.ConnectServer();
            // ȭ�� �ٲٱ�
            Manager.scene.ChangeGameState(this, Define.GameState.SettingScene);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainScene_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetRoomInfoScene modal = new GetRoomInfoScene();
            modal.ShowDialog(); 
        }
    }
}
