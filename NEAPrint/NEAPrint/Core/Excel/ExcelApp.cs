/***************************************************************************
*projectname:NEAPrint.Excel
*classname:ExcelApp
*des:ExcelApp
*author:guandy   https://github.com/guandy/NEAPrint
*createtime:2019-04-11 09:25:17
*updatetime:2019-04-11 09:25:17
***************************************************************************/
using System;
using System.Data;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;

namespace NEAPrint
{
	public class ExcelApp 
	{
        /// <summary>
        /// ExcelӦ�ó���
        /// </summary>
        private Excel.Application _excelApp;                          
        /// <summary>
        /// Ĭ��ֻ��һ������Open()����
        /// </summary>
        private Excel.Workbook _excelWorkbook;
        /// <summary>
        /// ��ӡ��Ԥ��ʱ�Ƿ�Ҫ��ʾExcel����
        /// </summary>
        private bool _IsExcelAppVisibled;
        /// <summary>
        /// ��ӡԤ��Excel����ı�����
        /// </summary>                      
        private string _FormCaption;
        /// <summary>
        /// ʵ������������,excel�ն���
        /// </summary>
        private object oMissing = System.Reflection.Missing.Value; 


        /// <summary>
        /// ExcelӦ�ó���
        /// </summary>
        public Excel.Application Application
        {
            get
            {
                return _excelApp;
            }
        }

        /// <summary>
        /// Excel��Ĭ��ֻ��һ������Open()����
        /// </summary>
        public Excel.Workbook Workbooks
        {
            get
            {
                return _excelWorkbook;
            }
        }

        /// <summary>
        /// ��ӡ��Ԥ��ʱ�Ƿ�Ҫ��ʾExcel����
        /// </summary>
        public bool IsExcelAppVisibled
        {
            get
            {
                return _IsExcelAppVisibled;
            }
            set
            {
                _IsExcelAppVisibled = value;
            }
        }

        /// <summary>
        /// ��ӡԤ��Excel����ı�����
        /// </summary>
        public string FormCaption
        {
            get
            {
                return _FormCaption;
            }
            set
            {
                _FormCaption = value;
            }
        }

        /// <summary>
        /// ������Excel�µ�ʵ��
        /// </summary>
        public ExcelApp()
        {
            //��ӡ��Ԥ��ʱExcel��ʾ
            _IsExcelAppVisibled = false;               
            _FormCaption = "��ӡԤ��";

            try
            {
                _excelApp = new Excel.ApplicationClass();
            }
            catch (System.Exception ex)
            {
                throw new ExcelCreateException("����Excel��ʵ��ʱ������ϸ��Ϣ��" + ex.Message);
            }
            //�رճ�������Excel�ļ�ʱ��������ʾ�Ƿ�Ҫ�����޸�
            _excelApp.DisplayAlerts = false;            
        }

        #region �򿪹ر�

        /// <summary>
        /// �������й�����ģ��򿪣����ָ����ģ�岻���ڣ�����Ĭ�ϵĿ�ģ��
        /// </summary>
        /// <param name="excelFileName">����ģ��Ĺ������ļ���</param>
        public void Open(string excelFileName)
        {
            if (System.IO.File.Exists(excelFileName))
            {
                try
                {
                    _excelWorkbook = _excelApp.Workbooks.Add(excelFileName);
                }
                catch (System.Exception ex)
                {
                    throw new ExcelOpenException("��Excelʱ������ϸ��Ϣ��" + ex.Message);
                }
            }
            else
            {
                Open();
            }
        }


        /// <summary>
        /// ��Excel��������Ĭ�ϵ�Workbooks��
        /// </summary>
        /// <returns></returns>
        public void Open()
        {
            try
            {
                _excelWorkbook = _excelApp.Workbooks.Add(oMissing);
            }
            catch (System.Exception ex)
            {
                throw new ExcelOpenException("��Excelʱ������ϸ��Ϣ��" + ex.Message);
            }

        }

        /// <summary>
        /// �ر�
        /// </summary>
        public void Close()
        {
            _excelApp.Workbooks.Close();
            _excelWorkbook = null;
            _excelApp.Quit();
            _excelApp = null;
            oMissing = null;
            System.GC.Collect();//�ͷ�GC,������Excel����
        }
        #endregion

        /// <summary>
        /// ��ʾExcel
        /// </summary>
        public void ShowExcel()
        {
            _excelApp.Visible = true;
        }

        /// <summary>
        /// ��ӡԤ�������Ҫ��ʾExcel���ڣ�IsVisibledExcel = true
        /// </summary>
        public void PrintPreview()
        {
            _excelApp.Caption = _FormCaption;
            _excelApp.Visible = true;

            try
            {
                _excelApp.WindowState = Excel.XlWindowState.xlMaximized;//���չʾ
                _excelApp.ActiveWorkbook.PrintPreview(oMissing);
            }
            catch { }

            _excelApp.Visible = this.IsExcelAppVisibled;

        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            _excelApp.Visible = this.IsExcelAppVisibled;

            object oMissing = System.Reflection.Missing.Value;  //ʵ������������
            try
            {
                _excelApp.ActiveWorkbook.PrintOut(oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
            }
            catch
            {
            }
        }
    }
}