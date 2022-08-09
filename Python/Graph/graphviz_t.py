'''
安装：
    1.  先安装 graphviz 程序，并且将bin目录添加到环境变量：https://graphviz.org/download/
        否则会报错：failed to execute WindowsPath('dot'), make sure the Graphviz executables are on your systems' PATH
    2.  安装 graphviz 模块
        pip install graphviz
'''

from graphviz import Digraph

def test1():
    gz=Digraph("基于环境容量的水污染物控制方案",'comment',None,None,'png',None,"UTF-8",
            {'rankdir':'TB'},
            {'color':'black','fontcolor':'black','fontname':'FangSong','fontsize':'16','style':'rounded','shape':'box'},
            {'color':'#999999','fontcolor':'#888888','fontsize':'10','fontname':'FangSong'},None,False)

    gz.node('0','水污染问题分析')
    gz.node('1','水污染、水质相应模型及计算')
    gz.node('2','水环境容量计算')

    gz.node('3','现状污染物贡献率分析')

    gz.node('4','污染物消减量计算')

    gz.node('5','水环境达标控制方案分析')

    gz.node('a','产业结构调整方案')
    gz.node('b','工业点源控制方案')
    gz.node('c','生活源控制方案')
    gz.node('d','农业面源控制方案')
    gz.node('e','生态修复方案')
    gz.node('f','河网综合整治方案')

    gz.node('6','区域综合整治措施')

    a=set(['01'])
    b=set(['02'])

    c=set(['13'])
    d=set(['23'])

    e=set(['34'])
    f=set(['45'])

    g=set(['5a'])
    h=set(['5b'])
    i=set(['5c'])
    j=set(['5d'])
    k=set(['5e'])
    l=set(['5f'])

    m=set(['a6'])
    n=set(['b6'])
    o=set(['c6'])
    p=set(['d6'])
    q=set(['e6'])
    r=set(['f6'])

    gz.edges(a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r)
    #gz.edges(a|b)


    print(gz.source)
    gz.view()

def test2():
    dot=Digraph(comment='first graphy',filename='Graph/firstGraph',format='png',)
    dot.graph_attr['bgcolor']='gray'
    dot.graph_attr['labeljust']='center'
    dot.graph_attr['margin']='0.75'
    dot.node('a', shape='box', fillcolor='lightblue',style='rounded,filled',label='开始',fontname='Microsoft YaHei')
    dot.node('b', shape='parallelogram', fillcolor='lightblue',style='filled',label='请输入数据 n ',fontname='Microsoft YaHei')
    dot.edge('a','b',arrowhead='vee')
    dot.node('c', shape='rectangle', fillcolor='lightblue',style='filled',label='i=1,s=0',fontname='Microsoft YaHei')
    dot.edge('b','c',arrowhead='vee')
    dot.node('d', shape='diamond', fillcolor='lightblue',style='filled',label='i<n',fontname='Microsoft YaHei')
    dot.edge('c','d',arrowhead='vee')
    dot.node('e', shape='rectangle', fillcolor='lightblue',style='filled',label='s+=i',fontname='Microsoft YaHei')
    dot.edge('d','e',label='Yes',arrowhead='vee')
    dot.node('f', shape='rectangle', fillcolor='lightblue',style='filled',label='i+=1',fontname='Microsoft YaHei')
    dot.edge('e','f',arrowhead='vee')
    dot.edge('f','d',arrowhead='vee')
    dot.node('g', shape='parallelogram', fillcolor='lightblue',style='filled',label='打印 s 的值',fontname='Microsoft YaHei')
    dot.edge('d','g',label='No',arrowhead='vee')
    dot.node('h', shape='parallelogram', fillcolor='lightblue',style='rounded,filled',label='结束',fontname='Microsoft YaHei')
    dot.edge('g','h',arrowhead='vee')
    dot.view()

if __name__ == '__main__':
    test2()
