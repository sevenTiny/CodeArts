# coding=utf-8
import sys
sys.path.append('..')

reload(sys)
sys.setdefaultencoding('utf8')

from GPIO.NixieTube import Yang4
import time


y4 = Yang4(35,16,22,32,31,36,38,33,37,12,18,40)

for i in range(0,300):
    y4.Display('LOVE.')
    time.sleep(0.005)