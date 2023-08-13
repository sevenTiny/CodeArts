from datetime import datetime
import sys, os

sys.path.append(
    os.path.abspath(os.path.dirname(os.path.dirname(
        os.path.abspath(__file__)))))
import smtplib
from email.mime.text import MIMEText
from config import settings as st


def send_mail_plain(receivers, subject, content):
    #163邮箱服务器地址
    mail_host = 'smtp.163.com'
    #163用户名
    mail_user = st.mail_163_user
    #密码(部分邮箱为授权码)
    mail_pass = st.mail_163_password
    #邮件发送方邮箱地址
    sender = st.mail_163_sender
    #邮件接受方邮箱地址，注意需要[]包裹，这意味着你可以写多个邮件地址群发
    receivers = receivers
    #邮件内容设置
    message = MIMEText(content, 'plain', 'utf-8')
    #邮件主题
    message['Subject'] = subject
    #发送方信息
    message['From'] = sender
    #接收方信息
    message['To'] = receivers[0]

    #登录并发送邮件
    smtpObj = smtplib.SMTP()
    #连接到服务器
    smtpObj.connect(mail_host, 25)
    #登录到服务器
    smtpObj.login(mail_user, mail_pass)
    #发送
    smtpObj.sendmail(sender, receivers, message.as_string())
    #退出
    smtpObj.quit()


if __name__ == '__main__':
    receivers = ['seventiny@foxmail.com']
    subject = 'SevenTiny通知'
    content = f'这是邮件主体内容！发送时间:{datetime.now()}'
    send_mail_plain(receivers, subject, content)
    print('Sent successfully!')