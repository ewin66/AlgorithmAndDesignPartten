﻿/* ==============================================================================
* 命名空间：CSharpAlgorithm
* 类 名 称：SortExtention
* 创 建 者：Qing
* 创建时间：2018-05-06 12:16:51
* CLR 版本：4.0.30319.42000
* 保存的文件名：SortExtention
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2018. All rights reserved
* ==============================================================================*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* C#常用8种排序算法实现以及原理简介
 *  https://www.cnblogs.com/zhao-yi/p/7205145.html?utm_source=itdadao&utm_medium=referral
 */
namespace CSharpAlgorithm
{
    /* 排序算法是我们编程中遇到的最多的算法。目前主流的算法有8种。
     *   平均时间复杂度从高到低依次是：
     *      冒泡排序（o(n2)），选择排序（o(n2)），插入排序（o(n2)），堆排序（o(nlogn)），
     *      归并排序（o(nlogn)），快速排序（o(nlogn)）， 希尔排序（o(n1.25)），基数排序（o(n)）
     *   这些平均时间复杂度是参照维基百科排序算法罗列的。
     */
    public static class SortExtention
    {
        #region 冒泡排序 -- 冒泡排序是笔试面试经常考的内容，虽然它是这些算法里排序速度最慢的（汗），后面有测试为证。
        /****************************************************************************************************************************
         * 原理：
         *  从头开始，每一个元素和它的下一个元素比较，如果它大，就将它与比较的元素交换，否则不动。
         *  这意味着，大的元素总是在向后慢慢移动直到遇到比它更大的元素。所以每一轮交换完成都能将最大值冒到最后。
         ****************************************************************************************************************************
         * 已知一组无序数据a[1]、a[2]、……a[n]，需将其按升序排列。首先比较a[1]与a[2]的值，若a[1]大于a[2]则交换两者的值，否则不变。
         * 再比较a[2]与a[3]的值，若a[2]大于a[3]则交换两者的值，否则不变。再比较a[3]与a[4]，以此类推，最后比较a[n-1]与a[n]的值。
         * 这样处理一轮后，a[n]的值一定是这组数据中最大的。再对a[1]~a[n-1]以相同方法处理一轮，则a[n-1]的值一定是a[1]~a[n-1]中最大的。
         * 再对a[1]~a[n-2]以相同方法处理一轮，以此类推。共处理n-1轮后a[1]、a[2]、……a[n]就以升序排列了。
         * 降序排列与升序排列相类似，若a[1]小于a[2]则交换两者的值，否则不变，后面以此类推。 
         * 总的来讲，每一轮排序后最大（或最小）的数将移动到数据序列的最后，理论上总共要进行n(n-1）/2次交换。
         * 优点：稳定
         * 时间复杂度：理想情况下（数组本来就是有序的），此时最好的时间复杂度为o(n),最坏的时间复杂度(数据反序的)，此时的时间复杂度为o(n*n) 。
         * 冒泡排序的平均时间负责度为o(n*n).
         * 缺点：慢，每次只移动相邻的两个元素。
         */
        /// <summary>
        /// 冒泡排序，结果升序排列
        /// <para>调用：arry.BubbleSort();</para>
        /// </summary>
        /// <param name="arry">要排序的整数数组</param>
        public static void BubbleSort(this int[] arry)
        {
            for (int i = 0; i < arry.Length; i++)
            {
                for (int j = 0; j < arry.Length - 1 - i; j++)
                {
                    //比较相邻的两个元素，如果前面的比后面的大，则交换位置
                    if (arry[j] > arry[j + 1])
                    {
                        int temp = arry[j + 1];
                        arry[j + 1] = arry[j];
                        arry[j] = temp;
                    }
                }
            }
        }
        #endregion

        #region 快速排序 -- 快速排序是一种有效比较较多的高效排序。它包含了“分而治之”以及“哨兵”的思想。
        /****************************************************************************************************************************
         * 原理：
         *  从数列中挑选一个数作为“哨兵”，使比它小的放在它的左侧，比它大的放在它的右侧。
         *  将要排序是数列递归地分割到最小数列，每次都让分割出的数列符合“哨兵”的规则，自然就将数列变得有序。
         ****************************************************************************************************************************
         * 设要排序的数组是A[0]……A[N-1]，首先任意选取一个数据（通常选用数组的第一个数）作为关键数据，
         * 然后将所有比它小的数都放到它前面，所有比它大的数都放到它后面，这个过程称为一趟快速排序。
         * 值得注意的是，快速排序不是一种稳定的排序算法，也就是说，多个相同的值的相对位置也许会在算法结束时产生变动。
         一趟快速排序的算法是：
            1）设置两个变量i、j，排序开始的时候：i=0，j=N-1；
            2）以第一个数组元素作为关键数据，赋值给key，即key=A[0]；
            3）从j开始向前搜索，即由后开始向前搜索(j–)，找到第一个小于key的值A[j]，将A[j]和A[i]互换；
            4）从i开始向后搜索，即由前开始向后搜索(i++)，找到第一个大于key的A[i]，将A[i]和A[j]互换；
            5）重复第3、4步，直到i=j； (3,4步中，没找到符合条件的值，即3中A[j]不小于key,4中A[i]不大于key的时候改变j、i的值，
         *     使得j=j-1，i=i+1，直至找到为止。找到符合条件的值，进行交换的时候i， j指针位置不变。另外，i==j这一过程一定正好是i+或j-完成的时候，此时令循环结束）。
         */
        /// <summary>
        /// 快速排序
        /// <para>调用：arry.QuickSort(0, arry.Length-1 );</para>
        /// </summary>
        /// <param name="arry">要排序的数组</param>
        /// <param name="left">低位</param>
        /// <param name="right">高位</param>
        public static void QuickSort(this int[] arry, int left, int right)
        {
            //左边索引小于右边，则还未排序完成 　　
            if (left < right)
            {
                //取中间的元素作为比较基准，小于他的往左边移，大于他的往右边移 　　
                int middle = arry[(left + right) / 2];
                int i = left - 1;
                int j = right + 1;
                while (true)
                {
                    //移动下标，左边的往右移动，右边的向左移动
                    while (arry[++i] < middle && i < right) ;
                    while (arry[--j] > middle && j > 0) ;
                    if (i >= j)
                        break;
                    //交换位置
                    int number = arry[i];
                    arry[i] = arry[j];
                    arry[j] = number;

                }
                QuickSort(arry, left, i - 1);
                QuickSort(arry, j + 1, right);
            }
        }
        #endregion

        #region 直接插入排序 -- 插入排序是一种对于有序数列高效的排序。非常聪明的排序。只是对于随机数列，效率一般，交换的频率高。
        /****************************************************************************************************************************
         * 原理：
         *   通过构建有序数列，将未排序的数从后向前比较，找到合适位置并插入。
         ****************************************************************************************************************************
         * 每次从无序表中取出第一个元素，把它插入到有序表的合适位置，使有序表仍然有序。
         * 第一趟比较前两个数，然后把第二个数按大小插入到有序表中； 第二趟把第三个数据与前两个数从前向后扫描，把第三个数按大小插入到有序表中；
         * 依次进行下去，进行了(n-1)趟扫描以后就完成了整个排序过程。
         * 直接插入排序属于稳定的排序，最坏时间复杂性为O(n^2)，空间复杂度为O(1)。
         * 直接插入排序是由两层嵌套循环组成的。外层循环标识并决定待比较的数值。内层循环为待比较数值确定其最终位置。
         * 直接插入排序是将待比较的数值与它的前一个数值进行比较，所以外层循环是从第二个数值开始的。
         * 当前一数值比待比较数值大的情况下继续循环比较，直到找到比待比较数值小的并将待比较数值置入其后一位置，结束该次循环。
         * 值得注意的是，我们必需用一个存储空间来保存当前待比较的数值，因为当一趟比较完成时，
         * 我们要将待比较数值置入比它小的数值的后一位 插入排序类似玩牌时整理手中纸牌的过程。
         * 插入排序的基本方法是：每步将一个待排序的记录按其关键字的大小插到前面已经排序的序列中的适当位置，直到全部记录插入完毕为止。
         */
        /// <summary>
        /// 直接插入排序
        /// <para>调用：arry.InsertSort();</para>
        /// </summary>
        /// <param name="arry">要排序的数组</param>
        public static void InsertSort(this int[] arry)
        {
            //直接插入排序是将待比较的数值与它的前一个数值进行比较，所以外层循环是从第二个数值开始的
            for (int i = 1; i < arry.Length; i++)
            {
                //如果当前元素小于其前面的元素
                if (arry[i] < arry[i - 1])
                {
                    //用一个变量来保存当前待比较的数值，因为当一趟比较完成时，我们要将待比较数值置入比它小的数值的后一位 
                    int temp = arry[i];
                    int j = 0;
                    for (j = i - 1; j >= 0 && temp < arry[j]; j--)
                    {
                        arry[j + 1] = arry[j];
                    }
                    arry[j + 1] = temp;
                }
            }
        }
        #endregion

        #region 希尔排序 -- 希尔排序是插入排序的一种更高效的改进版本。我们知道插入排序：1.它对有序数列排序的效率是非常高的 2.要排序的数向前移动是一步步进行的导致插入排序效率低。希尔排序正是利用第一点，改善第二点，达到更理想的效果。
        /****************************************************************************************************************************
         * 原理：
         *   通过奇妙的步长，插入排序间隔步长的元素，随后逐渐缩短步长至1，实现数列的插入排序。
         ****************************************************************************************************************************
         * 希尔排序(Shell Sort)是插入排序的一种。也称缩小增量排序，是直接插入排序算法的一种更高效的改进版本。
         * 希尔排序是非稳定排序算法。该方法因DL．Shell于1959年提出而得名。

         * 希尔排序是基于插入排序的以下两点性质而提出改进方法的：
         * 插入排序在对几乎已经排好序的数据操作时，效率高，即可以达到线性排序的效率。
         * 但插入排序一般来说是低效的，因为插入排序每次只能将数据移动一位。

        基本思想：

         * 先取一个小于n的整数d1作为第一个增量，把文件的全部记录分组。所有距离为d1的倍数的记录放在同一个组中。
         * 先在各组内进行直接插入排序；然后，取第二个增量d2<d1重复上述的分组和排序，直至所取的增量 =1( < …<d2<d1)，
         * 即所有记录放在同一组中进行直接插入排序为止。
         * 该方法实质上是一种分组插入方法
         * 比较相隔较远距离（称为增量）的数，使得数移动时能跨过多个元素，
         * 则进行一次比[2] 较就可能消除多个元素交换。D.L.shell于1959年在以他名字命名的排序算法中实现了这一思想。
         * 算法先将要排序的一组数按某个增量d分成若干组，每组中记录的下标相差d.对每组中全部元素进行排序，
         * 然后再用一个较小的增量对它进行，在每组中再进行排序。当增量减到1时，整个要排序的数被分成一组，排序完成。
         * 一般的初次取序列的一半为增量，以后每次减半，直到增量为1。
         */
        /// <summary>
        /// 希尔排序
        /// <para>调用：arry.ShellSort();</para>
        /// </summary>
        /// <param name="arry">待排序的数组</param>
        public static void ShellSort(this int[] arry)
        {
            int length = arry.Length;
            for (int h = length / 2; h > 0; h = h / 2)
            {
                //here is insert sort
                for (int i = h; i < length; i++)
                {
                    int temp = arry[i];
                    if (temp < arry[i - h])
                    {
                        for (int j = 0; j < i; j += h)
                        {
                            if (temp < arry[j])
                            {
                                temp = arry[j];
                                arry[j] = arry[i];
                                arry[i] = temp;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 简单选择排序 -- 选择排序是我觉得最简单暴力的排序方式了。
        /****************************************************************************************************************************
         * 原理：
         *   找出参与排序的数组最大值，放到末尾（或找到最小值放到开头）
         ****************************************************************************************************************************
         * 设所排序序列的记录个数为n。i取1,2,…,n-1,从所有n-i+1个记录（Ri,Ri+1,…,Rn）中找出排序码最小的记录，
         * 与第i个记录交换。执行n-1趟 后就完成了记录序列的排序。
         * 在简单选择排序过程中，所需移动记录的次数比较少。最好情况下，
         * 即待排序记录初始状态就已经是正序排列了，则不需要移动记录。
         * 最坏情况下，即待排序记录初始状态是按逆序排列的，则需要移动记录的次数最多为3（n-1）。
         * 简单选择排序过程中需要进行的比较次数与初始状态下待排序的记录序列的排列情况无关。
         * 当i=1时，需进行n-1次比较；当i=2时，需进行n-2次比较；依次类推，
         * 共需要进行的比较次数是(n-1)+(n-2)+…+2+1=n(n-1)/2，即进行比较操作的时间复杂度为O(n^2)，
         * 进行移动操作的时间复杂度为O(n)。
         */
        /// <summary>
        /// 简单选择排序
        /// <para>调用：arry.SimpleSelectSort();</para>
        /// </summary>
        /// <param name="arry">待排序的数组</param>
        public static void SimpleSelectSort(this int[] arry)
        {
            for (int i = 0; i < arry.Length; i++)
            {
                var t = i;//最小数标记
                for (int j = i + 1; j < arry.Length; j++)
                {
                    if (arry[t] > arry[j])
                    {
                        t = j;
                    }
                }
                var tmp = arry[i];
                arry[i] = arry[t];
                arry[t] = tmp;
            }
        }
        #endregion

        #region 堆排序 -- 堆排序是根据堆这种数据结构设计的一种算法。堆的特性：父节点的值总是小于（或大于）它的子节点。近似二叉树。
        /****************************************************************************************************************************
         * 原理：
         *   将数列构建为最大堆数列（即父节点总是最大值），将最大值（即根节点）交换到数列末尾。
         *   这样要排序的数列数总和减少，同时根节点不再是最大值，调整最大堆数列。如此重复，最后得到有序数列。
         ****************************************************************************************************************************
         * 堆排序(Heapsort)是指利用堆积树（堆）这种数据结构所设计的一种排序算法，
         * 它是选择排序的一种。可以利用数组的特点快速定位指定索引的元素。
         * 堆分为大根堆和小根堆，是完全二叉树。大根堆的要求是每个节点的值都不大于其父节点的值，
         * 即A[PARENT[i]] >= A[i]。在数组的非降序排序中，需要使用的就是大根堆，
         * 因为根据大根堆的要求可知，最大的值一定在堆顶。
         */
        /// <summary>
        /// 堆排序
        /// <para>调用：arry.HeapSort(arry.Length);</para>
        /// </summary>
        /// <param name="arry">待排序的数组</param>
        /// <param name="top"></param>
        public static void HeapSort(this int[] arry, int top)
        {
            //List<int> topNode = new List<int>();

            for (int i = arry.Length / 2 - 1; i >= 0; i--)
            {
                HeapAdjust(arry, i, arry.Length);
            }

            for (int i = arry.Length - 1; i >= arry.Length - top; i--)
            {
                int temp = arry[0];
                arry[0] = arry[i];
                arry[i] = temp;
                HeapAdjust(arry, 0, i);
            }
        }

        /// <summary>
        /// 构建堆
        /// </summary>
        /// <param name="arry"></param>
        /// <param name="parent"></param>
        /// <param name="length"></param>
        private static void HeapAdjust(int[] arry, int parent, int length)
        {
            int temp = arry[parent];

            int child = 2 * parent + 1;

            while (child < length)
            {
                if (child + 1 < length && arry[child] < arry[child + 1]) child++;

                if (temp >= arry[child])
                    break;

                arry[parent] = arry[child];

                parent = child;

                child = 2 * parent + 1;
            }

            arry[parent] = temp;
        }

        #endregion

        #region 归并排序 -- 归并排序也是采用“分而治之”的方式。
        /****************************************************************************************************************************
         * 原理：
         *   将两个有序的数列，通过比较，合并为一个有序数列。 
         ****************************************************************************************************************************
         * 归并排序是建立在归并操作上的一种有效的排序算法,该算法是采用分治法（Divide and Conquer）的一个非常典型的应用。
         * 将已有序的子序列合并，得到完全有序的序列；即先使每个子序列有序，再使子序列段间有序
         * 。若将两个有序表合并成一个有序表，称为二路归并。
         * 归并过程为：比较a[i]和a[j]的大小，若a[i]≤a[j]，则将第一个有序表中的元素a[i]复制到r[k]中，
         * 并令i和k分别加上1；否则将第二个有序表中的元素a[j]复制到r[k]中，并令j和k分别加上1，
         * 如此循环下去，直到其中一个有序表取完，然后再将另一个有序表中剩余的元素复制到r中从下标k到下标t的单元。
         * 归并排序的算法我们通常用递归实现，先把待排序区间[s,t]以中点二分，接着把左边子区间排序，再把右边子区间排序，
         * 最后把左区间和右区间用一次归并操作合并成有序的区间[s,t]。

         * 归并操作(merge)，也叫归并算法，指的是将两个顺序序列合并成一个顺序序列的方法。
         * 如　设有数列{6，202，100，301，38，8，1}
         * 初始状态：6,202,100,301,38,8，1
         * 第一次归并后：{6,202},{100,301},{8,38},{1}，比较次数：3；
         * 第二次归并后：{6,100,202,301}，{1,8,38}，比较次数：4；
         * 第三次归并后：{1,6,8,38,100,202,301},比较次数：4；
         * 总的比较次数为：3+4+4=11,；
         * 逆序数为14；

         * 归并操作的工作原理如下：
         * 第一步：申请空间，使其大小为两个已经排序序列之和，该空间用来存放合并后的序列
         * 第二步：设定两个指针，最初位置分别为两个已经排序序列的起始位置
         * 第三步：比较两个指针所指向的元素，选择相对小的元素放入到合并空间，并移动指针到下一位置
         * 重复步骤3直到某一指针超出序列尾
         * 将另一序列剩下的所有元素直接复制到合并序列尾
         */
        /// <summary>
        /// 归并排序
        /// <para>调用：arry.MergeSort(0, arry.Length);</para>
        /// </summary>
        /// <param name="arry">待排序数组</param>
        /// <param name="first"></param>
        /// <param name="last"></param>
        public static void MergeSort(this int[] arry, int first, int last)
        {
            if (first + 1 < last)
            {
                int mid = (first + last) / 2;

                MergeSort(arry, first, mid);
                MergeSort(arry, mid, last);

                Merger(arry, first, mid, last);
            }
        }
        /// <summary>
        /// 归并
        /// </summary>
        /// <param name="arry"></param>
        /// <param name="first"></param>
        /// <param name="mid"></param>
        /// <param name="last"></param>
        private static void Merger(int[] arry, int first, int mid, int last)
        {
            Queue<int> tempV = new Queue<int>();
            //设置indexA，并扫描subArray1 [first,mid]
            //设置indexB,并扫描subArray2 [mid,last]
            int indexA = first;
            int indexB = mid;
            //在没有比较完两个子标的情况下，比较 v[indexA]和v[indexB]
            //将其中小的放到临时变量tempV中
            while (indexA < mid && indexB < last)
            {
                if (arry[indexA] < arry[indexB])
                {
                    tempV.Enqueue(arry[indexA]);
                    indexA++;
                }
                else
                {
                    tempV.Enqueue(arry[indexB]);
                    indexB++;
                }
            }
            //复制没有比较完子表中的元素
            while (indexA < mid)
            {
                tempV.Enqueue(arry[indexA]);
                indexA++;
            }
            while (indexB < last)
            {
                tempV.Enqueue(arry[indexB]);
                indexB++;
            }
            int index = 0;
            while (tempV.Count > 0)
            {
                arry[first + index] = tempV.Dequeue();
                index++;
            }
        }

        #endregion

        #region 基数排序 -- 基数排序是一种非比较型整数排序。“非比较型”是什么意思呢？因为它内部使用的是桶排序，而桶排序是非比较型排序。
        /****************************************************************************************************************************
         * 原理：
         *   取一定数量（数列中的最大值）的编好序号的桶，将数列每个数放进编号为它的桶里，
         *   然后将不是空的桶依次倒出来，就组成有序数列了。 
         ****************************************************************************************************************************
         * 基数排序（radix sort）属于“分配式排序”（distribution sort），又称“桶子法”（bucket sort）或bin sort，
         * 顾名思义，它是透过键值的部份资讯，将要排序的元素分配至某些“桶”中，藉以达到排序的作用，
         * 基数排序法是属于稳定性的排序，其时间复杂度为O (nlog(r)m)，其中r为所采取的基数，
         * 而m为堆数，在某些时候，基数排序法的效率高于其它的稳定性排序法。
         */
        /// <summary>
        /// 基数排序
        /// <para>约定：待排数字中没有0,如果某桶内数字为0则表示该桶未被使用,输出时跳过即可</para>
        /// <para>调用：arry.RadixSort();</para>
        /// </summary>
        /// <param name="arry">待排数组</param>
        /// <param name="arrayX">桶数组第一维长度</param>
        /// <param name="arrayY">桶数组第二维长度</param>
        public static void RadixSort(this int[] arry, int arrayX = 10, int arrayY = 100)
        {
            /* 最大数字不超过999999999...(array_x个9) */
            for (int i = 0; i < arrayX; i++)
            {
                int[,] bucket = new int[arrayX, arrayY];
                foreach (var item in arry)
                {
                    int temp = (item / (int)Math.Pow(10, i)) % 10;
                    for (int l = 0; l < arrayY; l++)
                    {
                        if (bucket[temp, l] == 0)
                        {
                            bucket[temp, l] = item;
                            break;
                        }
                    }
                }
                for (int o = 0, x = 0; x < arrayX; x++)
                {
                    for (int y = 0; y < arrayY; y++)
                    {
                        if (bucket[x, y] == 0) continue;
                        arry[o++] = bucket[x, y];
                    }
                }
            }
        }
        #endregion
    }
}
