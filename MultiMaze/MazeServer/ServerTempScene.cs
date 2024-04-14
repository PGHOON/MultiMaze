using System.Net.Sockets;
using System.Net;
using System.Text;
using MazeClient;

namespace MazeServer
{
    public partial class ServerTempScene : Form
    {
        List<Socket> clientList;
        public ServerTempScene()
        {
            InitializeComponent();

            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000);

            serverSocket.Bind(endPoint);
            serverSocket.Listen(20);

            serverSocket.BeginAccept(AcceptComplete, serverSocket);
            
            clientList = new List<Socket>();


        }
        int Count = 1;
        public  void AcceptComplete(IAsyncResult ar)
        {
            Socket serverSocket = ar.AsyncState as Socket; // AsyncState�� ���� �ڷḦ serverSocket�� �ű�
            Socket clientSocket = serverSocket.EndAccept(ar); //Begin ������ End�� �ؾ��� 

            Console.WriteLine(clientSocket.RemoteEndPoint);

            MessageBox.Show("���� - ���� �Ϸ�" + Count++);
            clientList.Add(clientSocket);

            // Ŭ���̾�Ʈ���� ȯ�� �޽��� ������
            string welcomeMessage = Count.ToString();
            byte[] dataToSend = Encoding.UTF8.GetBytes(welcomeMessage);
            clientSocket.BeginSend(dataToSend, 0, dataToSend.Length, SocketFlags.None, SendComplete, clientSocket);



            serverSocket.BeginAccept(AcceptComplete, serverSocket);

            byte[] buffer = new byte[256];
            clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveComplete, (clientSocket, buffer));

        }

        public void SendComplete(IAsyncResult ar)
        {
            Socket clientSocket = ar.AsyncState as Socket; // Ŭ���̾�Ʈ ���� ������
            int bytesSent = clientSocket.EndSend(ar); // ������ ���� �Ϸ�
            Console.WriteLine($"���� ����Ʈ ��: {bytesSent}");
        }

        public  void ReceiveComplete(IAsyncResult ar)
        {
            (Socket clientSocket, byte[] buffer) = ((Socket, byte[]))ar.AsyncState;
            int n1 = clientSocket.EndReceive(ar);
            if (n1 < 1)
            {
                Console.WriteLine("client disconnected");
                clientSocket.Dispose();
                return;
            }

            Console.WriteLine(Encoding.UTF8.GetString(buffer));

            byte[] buffer2 = new byte[256];
            clientSocket.BeginReceive(buffer2, 0, buffer2.Length, SocketFlags.None, ReceiveComplete, (clientSocket, buffer2));



        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
