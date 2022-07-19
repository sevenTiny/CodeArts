'''
代理检测
created: 2022年7月4日
author:7tiny
'''
from distutils.log import error
from functools import cache
from traceback import print_tb
import requests
from bs4 import BeautifulSoup


def validate(proxies):
    print(f"当前使用代理：{proxies.values()}")
    headers = {'User-Agent': 'curl/7.29.0'}

    try:
        print('正在检测 http 代理...')
        http_url = 'http://ip111.cn/'
        http_r = requests.get(http_url,
                              headers=headers,
                              proxies=proxies,
                              timeout=10)
        soup = BeautifulSoup(http_r.content, 'html.parser')
        result = soup.find(
            class_='card-body').get_text().strip().split('''\n''')[0]

        print(f"访问http网站使用代理成功：{result}")
    except Exception as ex:
        print(f'访问http网站使用代理失败：{ex}')

    try:
        print('正在检测 https 代理...')
        https_url = 'https://ip.cn'
        https_r = requests.get(https_url,
                               headers=headers,
                               proxies=proxies,
                               timeout=10)

        print(f"访问https网站使用代理成功：{https_r.json()}")
    except Exception as ex:
        print(f"访问https网站使用代理失败：{ex}")


if __name__ == '__main__':
    
    proxies = {
        "http": "http://103.148.72.126:80",
        "https": "https://197.255.209.34:8080",
    }

    validate(proxies)