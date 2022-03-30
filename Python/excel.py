import sys
import xlrd
import xlsxwriter
import os
import datetime
import xlwt
import xlwings

# 本实现参考：https://zhuanlan.zhihu.com/p/259583430
'''
【推荐】
xlsxwriter 组件实现
仅支持xlsx格式
仅支持【写】
可以处理xlwt下URL链接长度过长（255）无法写入的问题
安装 pip install xlsxwriter
使用 import xlsxwriter
'''


def use_xlsxwriter():
    # 创建新的workbook
    workbook = xlsxwriter.Workbook('./temp/xlsxwriter.xlsx')
    # URL过长情况可以通过 xlsxwriter.Workbook(end_xls).max_url_length= 10000000来解决

    # 创建新的sheet表
    worksheet = workbook.add_worksheet('sheet1')

    # 简单写入
    worksheet.write(0, 0, '单元格1')
    worksheet.write(0, 1, '单元格2')
    worksheet.write(0, 2, '单元格3')

    # 写url
    worksheet.write(1, 0, 'URL格式')
    worksheet.write_url(1, 1, 'http://www.baidu.com', string='百度')

    # 添加用于突出显示单元格的粗体格式
    bold = workbook.add_format({'bold': True})
    worksheet.write(2, 0, '加粗')
    worksheet.write(2, 1, 88.99, bold)

    # 日期格式
    date_format = workbook.add_format({'num_format': 'mmmm d yyyy'})
    date = datetime.datetime.strptime('2022-3-3', "%Y-%m-%d")
    worksheet.write(3, 0, '日期格式')
    worksheet.write_datetime(3, 1, date, date_format)

    # 保存
    workbook.close()


'''
xlrd 组件实现
支持xls、xlsx格式(xlrd1.2.0之后的版本不支持xlsx格式，支持xls格式)
支持【读】
【不支持新版本xlsx，新版本不推荐】
安装 pip install xlrd
使用 import xlrd
'''


def use_xlrd():
    # 先生成一个备用excel
    use_xlwt()

    # 文件名以及路径，如果路径或者文件名有中文给前面加一个 r
    workbook = xlrd.open_workbook('./temp/xlwt.xls')

    table = workbook.sheets()[0]  #通过索引顺序获取
    table = workbook.sheet_by_index(0)  #通过索引顺序获取
    table = workbook.sheet_by_name('sheet1')  #通过名称获取
    # 以上三个函数都会返回一个xlrd.sheet.Sheet()对象

    names = workbook.sheet_names()  #返回book中所有工作表的名字
    workbook.sheet_loaded('sheet1' or 0)  # 检查某个sheet是否导入完毕

    # 行操作

    # 获取该sheet中的行数，注，这里table.nrows后面不带().
    nrows = table.nrows
    rowx = 0
    # 返回由该行中所有的单元格对象组成的列表,这与tabel.raw()方法并没有区别。
    table.row(rowx)
    # 返回由该行中所有的单元格对象组成的列表
    table.row_slice(rowx)
    # 返回由该行中所有单元格的数据类型组成的列表；
    # 返回值为逻辑值列表，若类型为empy则为0，否则为1
    table.row_types(rowx, start_colx=0, end_colx=None)
    # 返回由该行中所有单元格的数据组成的列表
    table.row_values(rowx, start_colx=0, end_colx=None)
    # 返回该行的有效单元格长度，即这一行有多少个数据
    table.row_len(rowx)

    # 列操作

    # 获取列表的有效列数
    ncols = table.ncols
    colx = 0
    # 返回由该列中所有的单元格对象组成的列表
    table.col(colx, start_rowx=0, end_rowx=None)
    # 返回由该列中所有的单元格对象组成的列表
    table.col_slice(colx, start_rowx=0, end_rowx=None)
    # 返回由该列中所有单元格的数据类型组成的列表
    table.col_types(colx, start_rowx=0, end_rowx=None)
    # 返回由该列中所有单元格的数据组成的列表
    table.col_values(colx, start_rowx=0, end_rowx=None)

    # 单元格

    # 返回单元格对象
    print(table.cell(rowx, colx))
    # 返回对应位置单元格中的数据类型
    print(table.cell_type(rowx, colx))
    # 返回对应位置单元格中的数据
    print(table.cell_value(rowx, colx))


'''
【推荐】
xlwt 组件实现
仅支持xls格式
支持【写，修改】
URL格式有255长度限制
安装 pip install xlwt
使用 import xlwt
'''


def use_xlwt():
    # 创建新的workbook
    workbook = xlwt.Workbook(encoding='ascii')
    # 创建新的sheet表
    worksheet = workbook.add_sheet("sheet1")

    # 简单写入
    worksheet.write(0, 0, '单元格1')
    worksheet.write(0, 1, '单元格2')
    worksheet.write(0, 2, '单元格3')

    # 写url
    worksheet.write(1, 0, 'URL格式')
    worksheet.write(1, 1,
                    xlwt.Formula('HYPERLINK("http://www.baidu.com","百度")'))

    # 初始化样式
    style = xlwt.XFStyle()
    # 为样式创建字体
    font = xlwt.Font()
    font.name = 'Times New Roman'  #字体
    font.bold = True  #加粗
    font.underline = True  #下划线
    font.italic = True  #斜体
    # 设置样式
    style.font = font

    worksheet.write(2, 0, '样式单元格', style)
    worksheet.write(2, 1, '样式单元格', style)

    # 设置列宽
    worksheet.col(2).width = 556 * 20
    worksheet.write(2, 2, '这列比较宽')

    # 背景色
    pattern = xlwt.Pattern()
    # May be: NO_PATTERN, SOLID_PATTERN, or 0x00 through 0x12
    pattern.pattern = xlwt.Pattern.SOLID_PATTERN
    # May be: 8 through 63. 0 = Black, 1 = White, 2 = Red, 3 = Green, 4 = Blue, 5 = Yellow,
    # 6 = Magenta, 7 = Cyan, 16 = Maroon, 17 = Dark Green, 18 = Dark Blue, 19 = Dark Yellow ,
    # almost brown), 20 = Dark Magenta, 21 = Teal, 22 = Light Gray, 23 = Dark Gray, the list goes on...
    pattern.pattern_fore_colour = 5
    style = xlwt.XFStyle()
    style.pattern = pattern
    # 使用样式
    worksheet.write(3, 0, "背景色")
    worksheet.write(3, 1, "我有颜色", style)

    # 保存
    workbook.save("./temp/xlwt.xls")


'''
【不推荐】
xlwings 组件实现
https://docs.xlwings.org/en/stable/index.html
支持xls和xlsx格式
支持【读、写、修改】
可以和matplotlib以及pandas无缝连接，支持读写numpy、pandas数据类型，将matplotlib可视化图表导入到excel中
可以调用Excel文件中VBA写好的程序，也可以让VBA调用用Python写的程序
使用时会打开excel，有点莫名奇妙，不太好用【不推荐】
安装 pip install xlwings
使用 import xlwings
'''


def use_xlwings():
    # app = xlwings.App(visible=False, add_book=False)
    #新建工作簿 (如果不接下一条代码的话，Excel只会一闪而过，卖个萌就走了）
    # 练习的时候建议直接用下面这条
    wb = xlwings.Book('./temp/xlwings.xlsx')
    # wb = app.books.add()
    # 创建sheet表
    worksheet = wb.sheets[0]
    # 写入第一行第一列
    worksheet.range('A1').value = '单元格1'
    # 写入第二行
    # worksheet.range(2, 0).value = '单元格2'
    # worksheet.range[2, 1].value = '单元格3'
    # 按行插入：A3:D3分别写入1,2,3,4
    worksheet.range('A2').value = [1, 2, 3, 4]
    worksheet.range('D3:G3').value = [5, 6, 7, 8]
    # 按列插入
    worksheet.range('C4:C7').value = [1, 2, 3, 4]

    # 保存
    wb.save('./temp/xlwings.xlsx')
    # 关闭（可省略）
    wb.close()
    # 退出Excel
    app.quit()

    # 读
    app = xlwings.App(visible=True, add_book=False)
    wb = app.books.open('./temp/xlwings.xlsx')
    # 练习的时候建议直接用下面这条
    # 这样的话就不会频繁打开新的Excel
    # wb = xlwings.Book('./temp/xlwings.xlsx')
    # 找到sheet
    worksheet = wb.sheets[0]
    # 读A1的值
    a = worksheet.range('A1').value
    print(str(a))
    # 读多值到列表
    listb = worksheet.range('D3:G3').value
    print(str(listb))

    # 关闭（可省略）
    wb.close()
    # 退出Excel
    app.quit()


if __name__ == '__main__':

    if not os.path.exists('temp'):
        os.mkdir('temp')

    # xlsxwriter 实现方式
    # use_xlsxwriter()

    # use_xlrd 实现方式
    # use_xlrd()

    # use_xlwt 实现方式
    # use_xlwt()

    # use_xlwings 实现方式
    # use_xlwings()

    print('finished!')
    sys.stdout.flush()