# https://pandas.pydata.org/docs/index.html
import imp

import os
from numpy import int32
import pandas as pd
'''
DataFrame
'''


def get_df():
    df = pd.DataFrame({
        "Name":
        ["苏洵", "苏轼", "苏辙", "张柬之", "王羲之", '白居易', '杜甫', '李白', '陶渊明', '张择端'],
        "Age": [69, 80, 70, 50, 60, 77, 75, 88, 90, 56],
        "Sex": ["m", "m", "m", "m", "m", "f", "f", "f", "f", "f"],
        "Score": [100, 100, 99, 66, 79, 89, 76, 45, 79, 49]
    })
    # 打印对齐
    pd.set_option('display.unicode.east_asian_width', True)
    print(df.info())
    print(df)
    return df


# 函数
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
    print('\n---查看前5行---')
    # print(df.head()) 不输入默认前5行
    print(df.head(5))
    print('\n---查看后3行---')
    print(df.tail(3))


# 过滤
def df_filter():
    df = get_df()
    print('\n---校验某列符合条件---')
    print(df['Age'] > 80)
    print('\n---筛选出符合条件的数据---')
    print(df[df['Age'] > 80])


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
    # df_caculate()
    # https://pandas.pydata.org/docs/getting_started/intro_tutorials/03_subset_data.html
    df_filter()
    # df_to_excel()
    # df_load_excel()
    # df_export_csv()
    # df_load_csv()
    None
