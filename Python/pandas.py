# https://pandas.pydata.org/docs/index.html
import pandas as pd

'''
Demo数据
'''
demo_data = {
    "Name": [
        "Braund, Mr. Owen Harris",
        "Allen, Mr. William Henry",
        "Bonnell, Miss. Elizabeth",
    ],
    "Age": [22, 35, 58],
    "Sex": ["male", "male", "female"],
}


def data_frame_view():
    df = pd.DataFram(demo_data())
    

if __name__ == 'main':
    None
