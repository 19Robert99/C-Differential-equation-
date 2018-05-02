using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
         
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        double e = 2.71;
        int u = 0;
        int p = 1;
        int[] arrayi = new int[21];
        double[] arrayT = new double[21];
        double[] arrayX = new double[21];
        double[] arrayYa = new double[21];
        double[] arrayY1 = new double[21];
        double[] arrayY2 = new double[22];
        double[] arraydY1 = new double[21];
        double[] arraydY2 = new double[21];
        double[] arraysY1 = new double[21];
        double[] arraysY2 = new double[21];
        double[] koef = new double[9];


        public void Result() 
        {
            double A = Convert.ToDouble(textBox1.Text);
            double k = Convert.ToDouble(textBox2.Text);
            double Y0 = Convert.ToDouble(textBox3.Text);
            double tau = Convert.ToDouble(textBox4.Text);
            double a0 = Convert.ToDouble(textBox5.Text);
            double a1 = Convert.ToDouble(textBox6.Text);
            double a2 = Convert.ToDouble(textBox7.Text);
            double a3 = Convert.ToDouble(textBox8.Text);

            double C1 = k * a0;
            double C2 = k * a1 * Math.Pow(A * a2 + 1, 2) / (Math.Pow(A * a2 + 1, 2) + Math.Pow(A * a3, 2));
            double C3 = -k * A * a1 * a3 / (Math.Pow(A * a2 + 1, 2) + Math.Pow(A * a3, 2));

            textBox9.Text = Convert.ToString(C1);
            textBox10.Text = Convert.ToString(C2);
            textBox11.Text = Convert.ToString(C3);

            double D1 = Math.Pow(e, (-tau / A));
            double D2 = A * k / tau * (1 - D1) - k * D1;
            double D3 = k - A * k / tau * (1 - D1);
            double D4 = 1 - (tau / A);
            double D5 = (k * tau) / A;

            textBox12.Text = Convert.ToString(D1);
            textBox13.Text = Convert.ToString(D2);
            textBox14.Text = Convert.ToString(D3);
            textBox15.Text = Convert.ToString(D4);
            textBox16.Text = Convert.ToString(D5);

            for (int i = 0; i < arrayi.Length; i++)
            {
                arrayi[i] = i + 1;

            }
            double j = 0;
            for (int i = 0; i < arrayT.Length; i++, j += tau)
            {
                arrayT[i] = j;


            }

            for (int i = 0; i < arrayX.Length; i++)
            {
                arrayX[i] = a0 + a1 * Math.Pow(e, a2 * arrayT[i]) * Math.Sin(a3 * arrayT[i]);


            }
            for (int i = 0; i < arrayYa.Length; i++)
            {
                arrayYa[i] = (Y0 - C1 - C3) * Math.Pow(e, -(arrayT[i] / A)) + C1 + Math.Pow(e, a2 * arrayT[i]) * (C2 * Math.Sin(a3 * arrayT[i]) + C3 * Math.Cos(a3 * arrayT[i]));

            }
            ////////////

            arrayY1[0] = 0;

            for (int i = 1; i < 21; i++, u++)
            {

                arrayY1[i] = D1 * arrayY1[u] + D2 * arrayX[u] + D3 * arrayX[i];

            }
            arrayY2[0] = 0;

            for (int i = 0; i < 21; i++, p++)
            {

                arrayY2[p] = D4 * arrayY2[i] + D5 * arrayX[i];

            }



            for (int i = 0; i < 21; i++)
            {
                arraydY1[i] = arrayYa[i] - arrayY1[i];


            }

            for (int i = 0; i < arraydY2.Length; i++)
            {
                arraydY2[i] = arrayYa[i] - arrayY2[i];


            }

            double Ymin = arrayYa[0];
            for (int i = 0; i < arrayYa.Length; i++)
            {
                if (arrayYa[i] < arrayYa[0])
                {
                    Ymin = arrayYa[i];
                }
            }
            textBox17.Text = Convert.ToString(Ymin);

            double Ymax = arrayYa[0];
            for (int i = 0; i < arrayYa.Length; i++)
            {
                if (Ymax < arrayYa[i])
                {
                    Ymax = arrayYa[i];
                }

            }
            textBox18.Text = Convert.ToString(Ymax);

            for (int i = 0; i < arraysY1.Length; i++)
            {
                arraysY1[i] = arraydY1[i] /(Ymax - Ymin);

                 
            }
            for (int i = 0; i < arraysY2.Length; i++)
            {
                arraysY2[i] = arraydY2[i] / (Ymax - Ymin);


            }
            for (int i = 0; i < arrayi.Length; i++)
            {
                dataGridView1.Rows.Add(arrayi[i], arrayT[i], arrayX[i], arrayYa[i], arrayY1[i], arrayY2[i], arraydY1[i], arraydY2[i], arraysY1[i], arraysY2[i]);
            }



        }
        public void MyChart()
        {

            
            chart1.ChartAreas[0].AxisX.ScaleView.Zoom(0, 10);
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            
            chart1.ChartAreas[0].AxisY.ScaleView.Zoom(0, 2);
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Minimum = 0;




            for (int i = 0; i < 21; i++)
            {
               
                chart1.Series[0].Points.AddXY(arrayT[i], arrayX[i]);
                  chart1.Series[1].Points.AddXY( arrayT[i],arrayYa[i]);
            } 
        }
        public void MyChart2()
        {   
            chart1.ChartAreas[0].AxisX.ScaleView.Zoom(0, 10);
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            
            chart1.ChartAreas[0].AxisY.ScaleView.Zoom(0, 2);
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
           
            


            for (int i = 0; i < 21; i++)
            {
                // chart1.Series[0].Points.AddXY(i, Math.Sin(i));
                chart1.Series[0].Points.AddXY(arrayT[i], arrayX[i]);
                chart1.Series[1].Points.AddXY(arrayT[i], arrayYa[i]);
                chart1.Series[2].Points.AddXY(arrayT[i], arrayY1[i]);
                chart1.Series[3].Points.AddXY(arrayT[i], arrayY2[i]);

            }
        }





        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void Расчет_Click(object sender, EventArgs e)
        {
            Result();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label21.Text = " X = a0 + a1 * exp(a2 * τ)* sin(a3 * τ)";
            MyChart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            label21.Text = "Аналитическое и численные решения";
            MyChart2();
        } 

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

       

        private void openFileDialog1_FileOk_1(object sender, CancelEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
            


        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }

     

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(openFileDialog1.FileName);
                while (!streamReader.EndOfStream)
                {
                    for (int i = 0; i < koef.Length; i++)
                    {
                        koef[i] = Convert.ToDouble(streamReader.ReadLine());
                    }
                    textBox1.Text = Convert.ToString(koef[0]);
                    textBox2.Text = Convert.ToString(koef[1]);
                    textBox3.Text = Convert.ToString(koef[2]);
                    textBox4.Text = Convert.ToString(koef[3]);
                    textBox5.Text = Convert.ToString(koef[4]);
                    textBox6.Text = Convert.ToString(koef[5]);
                    textBox7.Text = Convert.ToString(koef[6]);
                    textBox8.Text = Convert.ToString(koef[7]);


                }
                streamReader.Close();



            }
        }

        private void очиститьГрафикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
        }

        private void очиститьТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();
            u = 0;
            p = 1;
            Array.Clear(arrayi, 0, arrayi.Length);
            Array.Clear(arrayT, 0, arrayi.Length);
            Array.Clear(arrayX, 0, arrayi.Length);
            Array.Clear(arrayYa, 0, arrayi.Length);
            Array.Clear(arrayY1, 0, arrayi.Length);
            Array.Clear(arrayY2, 0, arrayi.Length);
            Array.Clear(arraydY1, 0, arrayi.Length);
            Array.Clear(arraydY2, 0, arrayi.Length);
            Array.Clear(arraysY1, 0, arrayi.Length);
            Array.Clear(arraysY2, 0, arrayi.Length);
            Array.Clear(koef, 0, koef.Length);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            textBox18.Text = "";




        }

        private void оРазработчикеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Работа студента КСФ-3.2"+"\n"+"Талабишки Роберта");
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"С:\MyFile.txt", FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fs);

            try
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    
                    for (int i = 0; i < dataGridView1.Rows[j].Cells.Count; i++)

                    {
                       
                        streamWriter.Write(dataGridView1.Rows[j].Cells[i].Value + "     ");
                    }


                    streamWriter.WriteLine();
                } 

                streamWriter.Close();
                fs.Close();

                MessageBox.Show("Файл успешно сохранен");
            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении файла!");
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
