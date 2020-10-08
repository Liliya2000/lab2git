
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;




namespace Network
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }


      public  class Node
        {
            public float X, Y;
            public int i, j, RecoveryTime, Iflag, Rflag, ChangeToI, ChangeToR;
            public bool S = true, I, R, Delete;
            public Node(int i, int j, float X, float Y, bool S, bool I, bool R, int Iflag, int Rflag, int RecoveryTime, bool Delete)
            {
                this.i = i;
                this.j = j;
                this.X = X;
                this.Y = Y;
                this.S = S;
                this.I = I;
                this.R = R;
                this.ChangeToI = ChangeToI;
                this.ChangeToR = ChangeToR;
                this.Iflag = Iflag;
                this.Rflag = Rflag;
                this.RecoveryTime = RecoveryTime;
                this.Delete = Delete;
                
            }
            public List<Node> Near = new List<Node>();

            public Node(List<Node> Node)
            {
                this.Near = Node;
            }

        }
        public static Graphics g; 
       public static  List<Node> Nodes = new List<Node>();

        public static int size;

        private void Draw1_Click(object sender, EventArgs e)
        {
            
            Nodes.Clear();
            int n = Convert.ToInt32(textBox1.Text);
            int m = n, Length;
            size = n*m;
            S = size;
            label11.Text = Convert.ToString(size);


            int height = pictureBox1.Height;
            int width = pictureBox1.Width;
            if (height / (n + 1) > Width / (m + 1))
                Length = width / (m + 1);
            else
                Length = height / (n + 1 );

            int[] DotsX = new int[m];
            int[] DotsY = new int[n];
            for (var i = 0; i < m; i++)
                DotsX[i] = Length / 2 + Length * i;
            for (var i = 0; i < n; i++)
                DotsY[i] = Length / 2 + Length * i;


            for (var i = 0; i < n; i++)
                for (var j = 0; j < m; j++)
                {

                    Nodes.Add(new Node(i, j, DotsX[j], DotsY[i], true, false, false, 0, 0, 0, false));
                
                }

           // Graphics g;    //  графический объект — некий холст
            Bitmap buf;  //  буфер для Bitmap-изображения
            buf = new Bitmap(pictureBox1.Width, pictureBox1.Height);  // с размерами
                                                                      // g = Graphics.FromImage(buf);   // инициализация g
            g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);

            SolidBrush mySolidBrush = new SolidBrush(Color.Yellow);
            Pen lin = new Pen(Color.Black, 3);

            int r = Length/4;
            if (checkBox1.Checked == true)
                for (int i = 0; i < Nodes.Count; i++)
                {
                    System.Threading.Thread.Sleep(trackBar1.Value);
                    g.DrawString("(" + Nodes[i].i + "|" + Nodes[i].j + ")", new Font("Arial", r), Brushes.Black, Nodes[i].X + 5, Nodes[i].Y - r);
                    Nodes[i].ChangeToI = 0; Nodes[i].ChangeToR = 0;
                }

            for (int i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i].j != m - 1)
                {
                    Nodes[i].Near.Add(Nodes[i + 1]);
                    g.DrawLine(lin, Nodes[i].X + 5, Nodes[i].Y + 5, Nodes[i + 1].X + 5, Nodes[i + 1].Y + 5);

                }
                if (Nodes[i].i != m - 1) 
                Nodes[i].Near.Add(Nodes[i + m]);
                  
                
            }

            for (int i = Nodes.Count - 1; i > 0; i--)
            {
                if (Nodes[i].j != 0)
                
                    Nodes[i].Near.Add(Nodes[i - 1]);
                
                if (Nodes[i].i != 0)
                {
                    Nodes[i].Near.Add(Nodes[i - m]);
                    g.DrawLine(lin, Nodes[i].X + 5, Nodes[i].Y + 5, Nodes[i - m ].X +5, Nodes[i - m ].Y + 5);

                }
            }

            for (var i = 0; i < Nodes.Count; i++)
                g.FillEllipse(mySolidBrush, Nodes[i].X, Nodes[i].Y, 10, 10);
            mySolidBrush.Dispose();
            

            
        }
        public static int I = 0;
        public static int  S = size;
        public static int R = 0;
        public static int N;
        public static int iter;
        public static int TI;



       

        private void button2_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            SolidBrush mySolidBrushR = new SolidBrush(Color.Red);
            SolidBrush mySolidBrushG = new SolidBrush(Color.Green);
            SolidBrush mySolidBrushY = new SolidBrush(Color.Yellow);
            int b = Convert.ToInt32(maskedTextBox1.Text);
            iter = 0;
            if (textBox4.Text == "") { MessageBox.Show("Вы не ввели время востановления", "Ошибка"); return; }
            int RecoveryTime = Convert.ToInt32(textBox4.Text);
            for (var i = 0; i < Nodes.Count; i++)
                Nodes[i].RecoveryTime = RecoveryTime;
            timer1.Interval = trackBar1.Value;
            timer1.Start();
            timer1.Tick += new EventHandler((o, ev) =>

             // System.Threading.Thread.Sleep(trackBar1.Value);

             {
                 //if (I != 0)
                 //{
                 for (int i = 0; i < Nodes.Count; i++)
                 {
                     if (Nodes[i].Delete == false)
                     {
                         if (Nodes[i].I == true)
                         {
                             for (int j = 0; j < Nodes[i].Near.Count; j++)
                             {
                                 if (Nodes[i].Near[j].S == true)
                                 {
                                     if (rand.Next(100) < b)
                                         Nodes[i].Near[j].Iflag = 1;
                                 }
                             }
                             if (Nodes[i].RecoveryTime > 0)
                                 Nodes[i].RecoveryTime--;
                             if (Nodes[i].RecoveryTime == 0)
                                 Nodes[i].Rflag = 1;
                         }
                     }
                 }


                 mySolidBrushR = new SolidBrush(Color.Red);
                 for (int i = 0; i < Nodes.Count; i++)
                 {
                     if (Nodes[i].Delete == false)
                     if (i < Nodes.Count)
                     {
                         if (Nodes[i].Iflag == 1)
                         {
                             Nodes[i].S = false;
                             Nodes[i].I = true;
                             Nodes[i].Iflag = 0;
                             //Nodes_sost[i].S = trueAdd(new Sost(true, false, false, 0));
                             //счетчик зараженных узлов
                             S--; I++;
                             g.FillEllipse(mySolidBrushR, Nodes[i].X, Nodes[i].Y, 10, 10);
                             Nodes[i].ChangeToR = iter;

                             }
                         else if (I > size)
                         { S++; I--; }

                         if (Nodes[i].Rflag == 1)
                         {
                             Nodes[i].I = false;
                             Nodes[i].R = true;
                             Nodes[i].Rflag = 0;
                             //счетчик зараженных узлов
                             I--; R++;
                             g.FillEllipse(mySolidBrushG, Nodes[i].X, Nodes[i].Y, 10, 10);
                             Nodes[i].ChangeToR = iter;
                         }

                     }
                 }

                 if (I != 0)
                     iter++;
                 label12.Text = Convert.ToString(S);
                 label13.Text = Convert.ToString(I);
                 label14.Text = Convert.ToString(R);
                 label4.Text = Convert.ToString(iter);
             }

            //}?//
           
                
            );
            iter++; label4.Text = Convert.ToString(iter);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //if (maskedTextBox1.Text == "") { MessageBox.Show("Вы не ввели вероятность заражаения", "Ошибка"); return; }


            iter = 0;
            float X = e.X;
            float Y = e.Y;
            float MinDest = 1000000;
            float dest = 0;
            for (int i = 0; i < Nodes.Count; i++)
            {
                //int N;
                dest = (float)(Math.Sqrt(Math.Pow(Nodes[i].X - X, 2) + Math.Pow(Nodes[i].Y - Y, 2)));
                if (dest < MinDest)
                {
                    MinDest = dest;
                    N = i;
                }
            }
            SolidBrush mySolidBrushB = new SolidBrush(Color.Blue);
            g.FillEllipse(mySolidBrushB, Nodes[N].X, Nodes[N].Y, 10, 10);
            mySolidBrushB.Dispose();
            // int b = Convert.ToInt32(maskedTextBox1.Text);

            // SolidBrush mySolidBrushR = new SolidBrush(Color.Red);

            // label12.Text = Convert.ToString(S);
            // label13.Text = Convert.ToString(I);
            // label14.Text = Convert.ToString(R);

            // if (Nodes[N].Delete == false)
            // {
            //     if (Nodes[N].S == true)
            //     {
            //         Nodes[N].S = false;
            //         Nodes[N].I = true;
            //         Nodes[N].R = false;
            //         g.FillEllipse(mySolidBrushR, Nodes[N].X, Nodes[N].Y, 10, 10);
            //         mySolidBrushR.Dispose();
            //         S--; I++;
            //         label12.Text = Convert.ToString(S);
            //         label13.Text = Convert.ToString(I);
            //         label14.Text = Convert.ToString(R);
            //         Nodes[N].ChangeToI = iter;
            //     }
            // }

            //// g.FillEllipse(mySolidBrushR, Nodes[N].X, Nodes[N].Y, 10, 10);
            // mySolidBrushR.Dispose();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            SolidBrush mySolidBrushY = new SolidBrush(Color.Yellow);
            S = size; I = 0; R = 0; iter = 0;
            N = 0;
            for (var i = 0; i < Nodes.Count; i++)
            {
                g.FillEllipse(mySolidBrushY, Nodes[i].X, Nodes[i].Y, 10, 10);
                Nodes[i].S = true;
                Nodes[i].I = false;
                Nodes[i].R = false;
                Nodes[i].Iflag = 0;
                Nodes[i].Rflag = 0;
                Nodes[i].ChangeToI = 0;
                Nodes[i].ChangeToR = 0;
            }
            mySolidBrushY.Dispose();


            label12.Text = Convert.ToString(S);
            label13.Text = Convert.ToString(I);
            label14.Text = Convert.ToString(R);
            label4.Text = Convert.ToString(iter);
        }

        private void button4_Click(object sender, EventArgs e)
        {
              Form2 newForm = new Form2();
              newForm.Show();
              
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (Nodes[N].S == true)
            {
                Nodes[N].S = false;
                Nodes[N].Iflag = 1;
                Nodes[N].R = false;
                

                SolidBrush mySolidBrushR = new SolidBrush(Color.Red);
                g.FillEllipse(mySolidBrushR, Nodes[N].X, Nodes[N].Y, 10, 10);
                mySolidBrushR.Dispose();
                S--; I++;
                label12.Text = Convert.ToString(S);
                label13.Text = Convert.ToString(I);
                label14.Text = Convert.ToString(R);
            }
            else 
                if (Nodes[N].I == true)
                {
                    Nodes[N].S = false;
                    Nodes[N].I = false;
                    Nodes[N].Rflag = 1;


                    SolidBrush mySolidBrushG = new SolidBrush(Color.Green);
                    g.FillEllipse(mySolidBrushG, Nodes[N].X, Nodes[N].Y, 10, 10);
                    mySolidBrushG.Dispose();
                    I--; R++;
                    label12.Text = Convert.ToString(S);
                    label13.Text = Convert.ToString(I);
                    label14.Text = Convert.ToString(R);


                } else 
                    if (Nodes[N].R == true)
                    {
                        Nodes[N].S = true;
                        Nodes[N].I = false;
                        Nodes[N].R = false;


                        SolidBrush mySolidBrushY = new SolidBrush(Color.Yellow);
                        g.FillEllipse(mySolidBrushY, Nodes[N].X, Nodes[N].Y, 10, 10);
                        mySolidBrushY.Dispose();
                        S++; R--;
                        label12.Text = Convert.ToString(S);
                        label13.Text = Convert.ToString(I);
                        label14.Text = Convert.ToString(R);
                    }
            if (Nodes[N].Rflag == 1) { Nodes[N].R = true; Nodes[N].Rflag = 0; }
            if (Nodes[N].Iflag == 1) { Nodes[N].I = true; Nodes[N].Iflag = 0; }
            //radioButton1.Checked = false;

            //return;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
            
                SolidBrush mySolidBrush = new SolidBrush(Color.White);
                g.FillEllipse(mySolidBrush, Nodes[N].X, Nodes[N].Y, 10, 10);
                mySolidBrush.Dispose();
                Pen lin = new Pen(Color.White, 3);
                for (int i = 0; i < Nodes[N].Near.Count; i++)
                {
                    g.DrawLine(lin, Nodes[N].X + 5, Nodes[N].Y + 5, Nodes[N].Near[i].X + 5 , Nodes[N].Near[i].Y + 5);
                }
                if (Nodes[N].S == true) S--;
                if (Nodes[N].I == true) I--;
                if (Nodes[N].R == true) R--;
            label12.Text = Convert.ToString(S);
            label13.Text = Convert.ToString(I);
            label14.Text = Convert.ToString(R);
            Nodes[N].Delete = true;

            //radioButton2.Checked = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            //if ((Nodes[N].S == false) && (Nodes[N].I == false) && (Nodes[N].R == false))
            
                SolidBrush mySolidBrush = new SolidBrush(Color.Yellow);
                SolidBrush mySolidBrushR = new SolidBrush(Color.Red);
                SolidBrush mySolidBrushG = new SolidBrush(Color.Green);

                Pen lin = new Pen(Color.Black, 3);
                for (int i = 0; i < Nodes[N].Near.Count; i++)
                {
                    g.DrawLine(lin, Nodes[N].X + 5, Nodes[N].Y + 5, Nodes[N].Near[i].X + 5, Nodes[N].Near[i].Y +5);
                }

                if (Nodes[N].S == true)
                    g.FillEllipse(mySolidBrush, Nodes[N].X, Nodes[N].Y, 10, 10);
                if (Nodes[N].I == true)
                    g.FillEllipse(mySolidBrushR, Nodes[N].X, Nodes[N].Y, 10, 10);
                if (Nodes[N].R == true)
                    g.FillEllipse(mySolidBrushG, Nodes[N].X, Nodes[N].Y, 10, 10);

                for (int i = 0; i < Nodes[N].Near.Count; i++)
                {
                    
                    if (Nodes[N].Near[i].S == true)
                        g.FillEllipse(mySolidBrush, Nodes[N].Near[i].X, Nodes[N].Near[i].Y, 10, 10);
                    if (Nodes[N].Near[i].I == true)
                        g.FillEllipse(mySolidBrushR, Nodes[N].Near[i].X, Nodes[N].Near[i].Y, 10, 10);
                    if (Nodes[N].Near[i].R == true)
                        g.FillEllipse(mySolidBrushG, Nodes[N].Near[i].X, Nodes[N].Near[i].Y, 10, 10);
                }
                if (Nodes[N].S == true) S++;
                if (Nodes[N].I == true) I++;
                if (Nodes[N].R == true) R++;
                mySolidBrush.Dispose();
                mySolidBrushR.Dispose();
                mySolidBrushG.Dispose();
                Nodes[N].Delete = false;
                label12.Text = Convert.ToString(S);
                label13.Text = Convert.ToString(I);
                label14.Text = Convert.ToString(R);

            }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //label 5, 17, 18, 19
            if (Nodes[N].S== true)
                label5.Text = Convert.ToString("Текущее состояние: S");
            if (Nodes[N].I == true)
                label5.Text = Convert.ToString("Текущее состояние: I");
            if (Nodes[N].R == true)
                label5.Text = Convert.ToString("Текущее состояние: R");
            label17.Text = Convert.ToString("Время изменения состояния узла: ");
            if (Nodes[N].S != true)
            {
                label18.Text = Convert.ToString("I: " + Nodes[N].ChangeToI);
                if (Nodes[N].ChangeToR != 0)
                    label19.Text = Convert.ToString("R: " + Nodes[N].ChangeToR);
            }
            else { label18.Text = Convert.ToString("Узел не был заражен и не менял своего состояния "); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "") { MessageBox.Show("Вы не ввели вероятность заражаения", "Ошибка"); return; }
            int b = Convert.ToInt32(maskedTextBox1.Text);

            SolidBrush mySolidBrushR = new SolidBrush(Color.Red);

            label12.Text = Convert.ToString(S);
            label13.Text = Convert.ToString(I);
            label14.Text = Convert.ToString(R);

            if (Nodes[N].Delete == false)
            {
                if (Nodes[N].S == true)
                {
                    Nodes[N].S = false;
                    Nodes[N].I = true;
                    Nodes[N].R = false;
                    g.FillEllipse(mySolidBrushR, Nodes[N].X, Nodes[N].Y, 10, 10);
                    mySolidBrushR.Dispose();
                    S--; I++;
                    label12.Text = Convert.ToString(S);
                    label13.Text = Convert.ToString(I);
                    label14.Text = Convert.ToString(R);
                    Nodes[N].ChangeToI = iter;
                }
            }

            // g.FillEllipse(mySolidBrushR, Nodes[N].X, Nodes[N].Y, 10, 10);
            mySolidBrushR.Dispose();
        }
    }


}

    
