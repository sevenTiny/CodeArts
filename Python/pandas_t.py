# https://pandas.pydata.org/docs/index.html
import os
from traceback import print_tb
from numpy import NaN, int32
import pandas as pd
import matplotlib.pyplot as plt
'''
DataFrame
'''


def get_df():
    df = pd.DataFrame({
        "Name":
        ["苏洵", "苏轼", "苏辙", "张柬之", "王羲之", '白居易', '杜甫', '李白', '李清照', '张择端'],
        "Age": [69, 81, 70, 50, 60, 43, 75, 88, 91, 56],
        "Sex": ['m', "m", "m", "m", "m", 'm', "m", 'm', "f", "m"],
        "BirthYear": [900, 921, 923, 681, 491, 660, 670, 650, 521, 1022],
        "Score": [100, 100, 99, 55, 79, 89, 76, 45, 79, 49]
    })
    # 打印对齐
    pd.set_option('display.unicode.east_asian_width', True)
    print(df.info())
    print('\n---获取df行列值---')
    print(df.shape)
    # 打印表格
    print(df)
    return df


# 计算函数
def df_caculate():
    df = get_df()
    print('\n---最小值---')
    print(df.min())
    print('\n---最大值---')
    print(df.max())
    print('\n---求和---')
    print(df.sum())
    print('\n---单列求和---')
    print(df['Score'].sum())
    print('\n---多列取中位数---')
    print(df[['Age', 'Score']].median())
    print('\n---聚合---')
    print(
        df.agg({
            'Age': ['min', 'max', 'median', 'skew'],
            'Score': ["min", "max", "median", "mean"]
        }))
    print('\n---groupby---')
    print(df.groupby('Sex').max())  # 按Sex分组后取每组最大
    print('\n---groupby2---')
    print(df.groupby('Sex')['BirthYear'].max())  # 按Sex分组后取每组的BirthYear最大
    print('\n---统计某列值有有多少相同的---')
    print(df['Score'].value_counts())
    print(df.groupby('Score')['Score'].count())


# 过滤
def df_filter():
    df = get_df()
    print('\n---查看前5行---')
    # print(df.head()) 不输入默认前5行
    print(df.head(5))
    print('\n---查看后3行---')
    print(df.tail(3))
    print('\n---查看分数列---')
    print(df['Score'])
    print('\n---校验某列符合条件---')
    print(df['Age'] > 80)
    print('\n---筛选Age > 80---')
    print(df[df['Age'] > 80])
    print('\n---筛选Age > 90 或 < 50---')
    print(df[(df['Age'] > 90) | (df['Age'] < 50)])
    print('\n---筛选Age 60-70之间---')
    print(df[df['Age'].isin([60, 70])])
    print('\n---获取1，3，4行---')
    print(df.iloc[[1, 3, 4]])
    print('\n---获取1行4列单个单元格的值---')
    print(df.iloc[1, 3])
    print('\n---获取1-2行0-1列多个单元格---')
    print(df.iloc[1:3, 0:2])
    print('\n---获取偶数行---')
    print(df.iloc[lambda x: x.index % 2 == 0])


# 替换
def df_replace():
    df = get_df()
    print('\n---替换字符串（不会替换原始df）---')
    ndf = df.replace('f', 'm')
    print(ndf)
    print('\n---替换字符串（替换原始df）---')
    df.replace('f', 'm', inplace=True)
    print(df)


# 编辑表格
def df_edit():
    df = get_df()
    print('\n---新加列---')
    df['ScorePass'] = df['Score'] > 60
    df['Score/Age'] = df['Score'] / df['Age']
    print(df)
    print('\n---换列名---')
    print(df.rename(columns={'Name': '姓名', 'Age': '年龄'}))


# 排序
def df_sort():
    df = get_df()
    print('\n---按Score排序---')
    print(df.sort_values(by='Score').head())
    print('\n---按Score，Age排序---')
    print(df.sort_values(by=['Score', 'Age']).head())


# 二维图
# pip install matplotlib
# https://pandas.pydata.org/docs/getting_started/intro_tutorials/04_plotting.html
# 其他图可以参考 plot_t.py
def df_plot():
    #解决中文显示问题
    plt.rcParams['font.sans-serif'] = ['KaiTi']  # 指定默认字体
    plt.rcParams['axes.unicode_minus'] = False  # 解决保存图像是负号'-'显示为方块的问题
    df = get_df()
    print('\n---折线图---')
    df.plot()  # 所有列参与折线
    # df['Score'].plot()    # 指定列参与折线
    name_list = df['Name'].values.tolist()
    plt.title('人员信息分布折线图')
    plt.xticks(df.index.values, name_list)
    plt.xlabel('姓名')
    plt.ylabel('详情')
    plt.show()


# 导出csv
def df_export_csv():
    df = get_df()
    df.to_csv('temp/pandas.csv', encoding='utf_8_sig')


# 加载csv
def df_load_csv():
    df = pd.read_csv('temp/pandas.csv', encoding='utf-8')
    pd.set_option('display.unicode.east_asian_width', True)
    print(df)


# 导出excel
def df_export_excel():
    df = get_df()
    df.to_excel('temp/pandas.xlsx', sheet_name='sheet1')


# 加载excel
def df_load_excel():
    df = pd.read_excel('temp/pandas.xlsx', sheet_name='sheet1')
    pd.set_option('display.unicode.east_asian_width', True)
    print(df)


'''
Series
'''


def get_series():
    # 默认指定
    s = pd.Series(range(10), dtype=int32)
    print(s)
    return s


# 函数
def serise_caculate():
    s = get_series()
    # print('describe=%s' % s.describe())
    print('\n---最小值---')
    print(s.min())
    print('\n---最大值---')
    print(s.max())
    print('\n---求和---')
    print(s.sum())
    print('\n---平均值---')
    print(s.mean())
    print('\n---标准偏差---')
    print(s.std())


if __name__ == '__main__':

    if not os.path.exists('temp'):
        os.mkdir('temp')

    # get_series()
    # serise_caculate()
    # get_df()
    # df_to_excel()
    # df_load_excel()
    # df_export_csv()
    # df_load_csv()
    # df_caculate()
    # df_filter()
    # df_replace()
    # df_plot()
    # df_edit()
    df_sort()
    None
