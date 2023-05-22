# coding: utf-8
import itchat
from itchat.content import *
import random
import json
import datetime
import time
import requests

# 需要参与聊天的群昵称
chatGroup = ['仨2', '歪，玖妖灵吗？', 'temp','temp2']
# 图灵的站点：http://www.turingapi.com/
# 这里填写图灵网站申请的机器人apiKey
tulingApiKey = ''
# 这里填写图灵网站注册的userid
tulingUserId = '417057'


# 根据长度返回响应时间
def sleepTime(msg):
    time.sleep(int(len(msg)/3)+1)

# 请求图灵api数据
def getTulingChatData(msg):
    uri = 'http://openapi.tuling123.com/openapi/api/v2'
    jsonData = {
        "reqType": 0,
        "perception": {
            "inputText": {
                "text": msg
            }
        },
        "userInfo": {
            "apiKey": tulingApiKey,
            "userId": tulingUserId
        }
    }

    response = requests.post(uri, json=jsonData)
    values = json.loads(response.text)['results']
    value = values[0]['values']['text']
    return value

#获取聊天回复
def getChatData(msg):
    #走图灵机器人
    return getTulingChatData(msg)

@itchat.msg_register([TEXT], isGroupChat=True)
def text_reply(msg):

    messageFrom = msg['User']['NickName']
    content = msg['Content']

    print('+ '+str(datetime.datetime.now()) +
          ' Message from:' + messageFrom + ',Message：'+content)

    sleepTime(content)

    if messageFrom in chatGroup:
        replyMsg = getChatData(content)
        print('sent message to '+messageFrom + ',Message:'+replyMsg)
        itchat.send(replyMsg, msg['FromUserName'])

itchat.auto_login(enableCmdQR=True, hotReload=True)
itchat.run()
