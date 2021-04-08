import requests
import os
import sys
import io
from bs4 import BeautifulSoup

'''获取连接内容'''


def GetLinkContent(link, encoding):
    res = requests.get(link)
    # print(res.encoding)
    content = res.content.decode(encoding, 'ignore')
    return content


'''获取热点资讯页面'''


def GetHotPointList():
    url = 'http://top.baidu.com/buzz?b=1'
    return GetLinkContent(url, 'gb2312')


''''通过热点咨询页面拿到文本及连接元祖列表'''


def GetTxtLinks():
    txt = GetHotPointList()
    txt_links = []

    html = BeautifulSoup(txt, features="html.parser")
    tds = html.find('table', {'class', 'list-table'}
                    ).find_all('td', {'class', 'keyword'})
    for td in tds:
        a = td.find('a')
        txt_links.append((a.get_text(), a.get('href')))
    return txt_links


'''将热点链接输出到文件'''


def OutPutTxtLinks():
    fileName = 'baiduhotpointlinks.txt'
    if(os.path.exists(fileName)):
        os.remove(fileName)
    for txt_link in GetTxtLinks():
        with open(fileName, 'a+', encoding="utf-8") as txt_linksFile:
            txt_linksFile.write(str(txt_link[0])+'||'+str(txt_link[1])+'\n')


'''循环获取分析单个热点资讯'''


def GetHotPoint(count):
    print('爬取目标：'+str(count)+'条.')
    currentCount = 0
    newsLinks = []
    # 获取热点链接列表
    txt_links = GetTxtLinks()
    if len(txt_links) > 0:
        for txtLink in txt_links:
            # 获取当前链接的页面信息
            linkContent = GetLinkContent(txtLink[1], 'utf-8')
            bs4 = BeautifulSoup(linkContent, features="html.parser")
            newsFrame = bs4.find('div', {'class': 'c-offset'})
            if newsFrame == None:
                continue
            cRow = newsFrame.find_all('div', {'class': 'c-row'})
            if cRow == []:
                continue
            for row in cRow:
                if '百家号' in row.get_text():
                    link = row.find('a').get('href')
                    newsLinks.append(link)
                    print('成功爬取一条:'+link)
                    currentCount += 1
                    break
            if currentCount >= count:
                break
    print('爬取结束！成功爬取到:'+str(len(newsLinks))+'条!')
    return newsLinks


'''输出热点资讯详情页信息'''


def OutPutHotPointsDetail(count):
    hotPointLinks = GetHotPoint(count)
    name = 1
    for link in hotPointLinks:
        html = GetLinkContent(link, 'utf-8')
        with open('detail'+str(name)+'.html', 'w', encoding='utf-8', errors='ignore') as hotpointHtml:
            hotpointHtml.write(html)
        name += 1


def GetHotPointsTitleSubject(count):
    hotPointLinks = GetHotPoint(count)
    titleContentList = []
    for link in hotPointLinks:
        detail = GetLinkContent(link, 'utf-8')
        bs4 = BeautifulSoup(detail, features="html.parser")
        title = bs4.find(
            'div', {'class': 'article-title'}).find('h2').get_text()
        content = bs4.find('div', {'class': 'article-content'})
        titleContentList.append((title, content))
    return titleContentList

def OutPutPointsTitleSubjectToHtml(count):
    titleSubject = GetHotPointsTitleSubject(count)
    fileName = 'titleSubject.txt'
    if(os.path.exists(fileName)):
        os.remove(fileName)
    for i in titleSubject:
        with open(fileName, 'a+', encoding="utf-8") as titleSubjectFile:
            titleSubjectFile.write(str(i[1])+'\n')

# main
try:
    args = sys.argv
    # 可以测试时输出
    # OutPutTxtLinks()

    # list:
    # 1.获取热点列表
    # 2.根据列表链接获取详情页列表
    # 3.根据详情页匹配百家号的文章链接
    # 4.根据百家好连接获取百家号详情页
    # 5.根据百家号详情页拿到文章详情
    # 6.组织新的文章输出内容

    # 输出热点详情页
    OutPutHotPointsDetail(5)

    # 获取热点资讯详情内容片段
    OutPutPointsTitleSubjectToHtml(5)

    print('succeed！')

except Exception as e:
    print(str(e))
sys.stdout.flush()
