﻿using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Lab12_17
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 7;
            dataGridView1.Rows[0].Cells[1].Value = "Обмен";
            dataGridView1.Rows[1].Cells[1].Value = "Выбор";
            dataGridView1.Rows[2].Cells[1].Value = "Включение";
            dataGridView1.Rows[3].Cells[1].Value = "Шелла";
            dataGridView1.Rows[4].Cells[1].Value = "Быстрая";
            dataGridView1.Rows[5].Cells[1].Value = "Линейная";
            dataGridView1.Rows[6].Cells[1].Value = "Встроенная";

            dataGridView1.Rows[0].Cells[0].Value = true;
            dataGridView1.Rows[1].Cells[0].Value = true;
            dataGridView1.Rows[2].Cells[0].Value = true;
            
        }

        private void table_cleaner() {


            for (int i = 2; i < dataGridView1.Rows[0].Cells.Count; i++) {
                dataGridView1.Rows[0].Cells[i].Value = null;
            }
            for (int i = 2; i < dataGridView1.Rows[1].Cells.Count; i++) {
                dataGridView1.Rows[1].Cells[i].Value = null;
            }
            for (int i = 2; i < dataGridView1.Rows[2].Cells.Count; i++) {
                dataGridView1.Rows[2].Cells[i].Value = null;
            }

        }

        private void sort_btn_Click(object sender, EventArgs e) {
            
            table_cleaner();
            
            int size = (int)numericUpDown1.Value;
            int[] array = GenerateArray(size);

            // Проверяем, какие сортировки выбраны
            if (Convert.ToBoolean(dataGridView1.Rows[0].Cells[0].Value)) // Обмен (пузырьковая)
                RunSort(array, BubbleSort, 0);

            if (Convert.ToBoolean(dataGridView1.Rows[1].Cells[0].Value)) // Выбор
                RunSort(array, SelectionSort, 1);

            if (Convert.ToBoolean(dataGridView1.Rows[2].Cells[0].Value)) // Включение
                RunSort(array, InsertionSort, 2);
        }
        private void RunSort(int[] originalArray, Func<int[], (int, int, long)> sortMethod, int rowIndex)
        {
            int[] array = (int[])originalArray.Clone();
            (int comparisons, int swaps, long time) = sortMethod(array);

            dataGridView1.Rows[rowIndex].Cells[2].Value = comparisons;
            dataGridView1.Rows[rowIndex].Cells[3].Value = swaps;
            dataGridView1.Rows[rowIndex].Cells[4].Value = time;
            dataGridView1.Rows[rowIndex].Cells[5].Value = IsSorted(array);
        }

        private int[] GenerateArray(int size)
        {
            Random rand = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
                array[i] = rand.Next(1, size);
            return array;
        }

        private (int, int, long) BubbleSort(int[] array)
        {
            int comparisons = 0, swaps = 0;
            bool flag;
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < array.Length - 1; i++) {
                
                flag = true;
                
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    comparisons++;
                    if (array[j] > array[j + 1])
                    {
                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        flag = false;
                        swaps++;
                        
                    }
                }

                if (flag) {
                    stopwatch.Stop();
                    return (comparisons, swaps, stopwatch.ElapsedMilliseconds);
                }
            }
            stopwatch.Stop();
            return (comparisons, swaps, stopwatch.ElapsedMilliseconds);
        }

        private (int, int, long) SelectionSort(int[] array)
        {
            int comparisons = 0, swaps = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < array.Length - 1; i++)
            {
                
                int minIndex = i;
                
                for (int j = i + 1; j < array.Length; j++)
                {
                    comparisons++;
                    if (array[j] < array[minIndex])
                        minIndex = j;
                }
                
                (array[i], array[minIndex]) = (array[minIndex], array[i]);
                swaps++;
            }
            stopwatch.Stop();
            return (comparisons, swaps, stopwatch.ElapsedMilliseconds);
        }
        private (int, int, long) InsertionSort(int[] array)
        {
            int comparisons = 0, swaps = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i - 1;


                while (j >= 0 && array[j] > key)
                {
                    comparisons++;
                    array[j + 1] = array[j];
                    j--;
                    swaps++;
                }
                array[j + 1] = key;
            }
            stopwatch.Stop();
            return (comparisons, swaps, stopwatch.ElapsedMilliseconds);
        }

        private bool IsSorted(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] > array[i])
                    return false;
            }
            return true;
        }

        private void exit_btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
