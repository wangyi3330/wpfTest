using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Carrot.BLL
{
    public class ToEcxel
    {
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">要合并单元格所在的sheet</param>
        /// <param name="rowstart">开始行的索引</param>
        /// <param name="rowend">结束行的索引</param>
        /// <param name="colstart">开始列的索引</param>
        /// <param name="colend">结束列的索引</param>
        public static void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }
        #region 定义单元格常用到样式的枚举
        public enum stylexls
        {
            月报头,
            月报底,
            月报内容,
            投缴头,
            补缴头,
            url,
            时间,
            数字,
            钱,
            百分比,
            中文大写,
            科学计数法,
            默认,
            表头,
            加粗,
            迟到早退标题,
            迟到早退标题加左线,
            迟到早退标题加左线左对齐,
            迟到早退内容,
            不含底线
        }
        #endregion

        #region 定义单元格常用到样式
        public static ICellStyle Getcellstyle(IWorkbook wb, stylexls str)
        {
            ICellStyle cellStyle = wb.CreateCellStyle();


            //定义几种字体    
            //也可以一种字体，写一些公共属性，然后在下面需要时加特殊的   
            IFont font10 = wb.CreateFont();
            font10.FontHeightInPoints = 10;
            font10.FontName = "宋体";
            font10.Boldweight = 700;

            IFont font12 = wb.CreateFont();
            font12.FontHeightInPoints = 10;
            font12.FontName = "宋体";
            font12.Boldweight = 700;

            IFont fontw = wb.CreateFont();
            fontw.FontName = "宋体";
            fontw.Boldweight = 700;

            IFont font14 = wb.CreateFont();
            font14.FontHeightInPoints = 14;
            font14.FontName = "宋体";
            font14.Boldweight = 700;

            IFont font = wb.CreateFont();
            font.FontHeightInPoints = 10;
            font.FontName = "宋体";

            IFont fontHT = wb.CreateFont();
            fontHT.FontHeightInPoints = 10;
            fontHT.FontName = "宋体";
            //font.Underline = 1;下划线    




            IFont fontcolorblue = wb.CreateFont();
            fontcolorblue.Color = HSSFColor.OliveGreen.Blue.Index;
            fontcolorblue.IsItalic = true;//下划线    
            fontcolorblue.FontName = "宋体";




            //边框    
            cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.None;
            cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.None;
            //边框颜色    
            cellStyle.BottomBorderColor = HSSFColor.OliveGreen.Black.Index;
            cellStyle.TopBorderColor = HSSFColor.OliveGreen.Black.Index;

            //背景图形，我没有用到过。感觉很丑    
            //cellStyle.FillBackgroundColor = HSSFColor.OLIVE_GREEN.BLUE.index;    
            //cellStyle.FillForegroundColor = HSSFColor.OLIVE_GREEN.BLUE.index;    

            // cellStyle.FillPattern = FillPatternType.NO_FILL;    
            //cellStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Blue.Index2;


            //水平对齐    
            cellStyle.Alignment = HorizontalAlignment.Left;


            //垂直对齐    
            cellStyle.VerticalAlignment = VerticalAlignment.Center;


            //自动换行    
            cellStyle.WrapText = true;


            //缩进;当设置为1时，前面留的空白太大了。希旺官网改进。或者是我设置的不对    
            cellStyle.Indention = 0;

            //上面基本都是设共公的设置    
            //下面列出了常用的字段类型    
            switch (str)
            {
                case stylexls.投缴头:
                    cellStyle.FillForegroundColor = HSSFColor.PaleBlue.Index;
                    cellStyle.FillPattern = FillPattern.SolidForeground;
                    cellStyle.SetFont(font12);
                    cellStyle.VerticalAlignment = VerticalAlignment.Top;
                    //水平对齐    
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    break;
                case stylexls.月报头:
                    cellStyle.FillForegroundColor = HSSFColor.Rose.Index;
                    cellStyle.FillPattern = FillPattern.SolidForeground;
                    cellStyle.SetFont(font10);
                    cellStyle.VerticalAlignment = VerticalAlignment.Top;
                    //水平对齐    
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    break;
                case stylexls.月报底:
                    cellStyle.FillForegroundColor = HSSFColor.Rose.Index;
                    cellStyle.FillPattern = FillPattern.SolidForeground;
                    cellStyle.SetFont(font10);
                    cellStyle.VerticalAlignment = VerticalAlignment.Top;
                    //水平对齐    
                    cellStyle.Alignment = HorizontalAlignment.Left;
                    break;
                case stylexls.月报内容:
                    cellStyle.FillForegroundColor = HSSFColor.White.Index;
                    cellStyle.FillPattern = FillPattern.SolidForeground;
                    cellStyle.SetFont(font10);
                    cellStyle.VerticalAlignment = VerticalAlignment.Top;
                    //水平对齐    
                    cellStyle.Alignment = HorizontalAlignment.Left;
                    break;
                case stylexls.补缴头:
                    cellStyle.FillForegroundColor = HSSFColor.Rose.Index;
                    cellStyle.FillPattern = FillPattern.SolidForeground;
                    cellStyle.SetFont(font12);
                    cellStyle.VerticalAlignment = VerticalAlignment.Top;
                    //水平对齐    
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    break;
                case stylexls.时间:
                    IDataFormat datastyle = wb.CreateDataFormat();


                    cellStyle.DataFormat = datastyle.GetFormat("yyyy/mm/dd");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.数字:
                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    cellStyle.SetFont(font);

                    cellStyle.VerticalAlignment = VerticalAlignment.Top;
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    break;
                case stylexls.钱:
                    IDataFormat format = wb.CreateDataFormat();
                    cellStyle.DataFormat = format.GetFormat("￥#,##0");
                    cellStyle.SetFont(font);
                    cellStyle.VerticalAlignment = VerticalAlignment.Top;
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    break;
                case stylexls.url:
                    fontcolorblue.Underline = FontUnderlineType.Single;
                    cellStyle.SetFont(fontcolorblue);
                    break;
                case stylexls.百分比:
                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.中文大写:
                    IDataFormat format1 = wb.CreateDataFormat();
                    cellStyle.DataFormat = format1.GetFormat("[DbNum2][$-804]0");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.科学计数法:
                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00E+00");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.默认:
                    cellStyle.SetFont(font);
                    break;
                case stylexls.表头:
                    cellStyle.SetFont(font14);

                    cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cellStyle.VerticalAlignment = VerticalAlignment.Top;
                    //水平对齐    
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    break;
                case stylexls.加粗:
                    cellStyle.SetFont(fontw);
                    //水平对齐    
                    cellStyle.Alignment = HorizontalAlignment.Left;
                    break;
                case stylexls.迟到早退标题:
                    cellStyle.SetFont(fontHT);
                    cellStyle.VerticalAlignment = VerticalAlignment.Top;
                    //水平对齐    
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    break;
                case stylexls.迟到早退内容:
                    cellStyle.SetFont(font);
                    cellStyle.VerticalAlignment = VerticalAlignment.Top;
                    //水平对齐    
                    cellStyle.Alignment = HorizontalAlignment.Left;
                    break;
                case stylexls.不含底线:
                    cellStyle.SetFont(font);
                    cellStyle.VerticalAlignment = VerticalAlignment.Top;
                    //水平对齐    
                    cellStyle.Alignment = HorizontalAlignment.Left;

                    cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.None;
                    break;
                case stylexls.迟到早退标题加左线:
                    cellStyle.SetFont(fontHT);

                    cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cellStyle.VerticalAlignment = VerticalAlignment.Top;

                    //水平对齐    
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    break;
                case stylexls.迟到早退标题加左线左对齐:
                    cellStyle.SetFont(fontHT);

                    cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cellStyle.VerticalAlignment = VerticalAlignment.Top;

                    //水平对齐    
                    cellStyle.Alignment = HorizontalAlignment.Left;
                    break;
            }
            return cellStyle;
        }
        #endregion
    }

}
