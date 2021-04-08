import requests

url = 'http://www.baidu.com'
data = {'username': 'user', 'password': '123456'}

result = requests.get(url)

txt = result.text

print(str(txt))
# response = requests.post(url, data)