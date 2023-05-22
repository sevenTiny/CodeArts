# coding=utf-8
import time
import datetime
import RPi.GPIO as GPIO
import sys
sys.path.append('..')
reload(sys)
sys.setdefaultencoding('utf8')

from GPIO.NixieTube import Yang4
import Adafruit_DHT
  

y4 = Yang4(35,16,22,32,31,36,38,33,37,12,18,40)
delay = 600

while(True):
    # time
    timenow = datetime.datetime.now()

    for i in range(0,delay):
        y4.Display(str(timenow.date().year)) 
        time.sleep(0.005)

    for i in range(0,delay):
        y4.Display(str(timenow.date().month).zfill(2)+'.'+str(timenow.date().day).zfill(2)) 
        time.sleep(0.005)
    
    for i in range(0,delay):
        y4.Display(str(timenow.time().hour).zfill(2)+'.'+str(timenow.time().minute).zfill(2)) 
        time.sleep(0.005)

    y4.Display('....')

    # Use read_retry method. This will retry up to 15 times to
    # get a sensor reading (waiting 2 seconds between each retry).
    # this is bcm code
    humidity, temperature = Adafruit_DHT.read_retry(Adafruit_DHT.DHT11, 4)
    print('time:{0},humidity:{1}%,temperature:{2}*C'.format(datetime.datetime.now(),humidity,temperature))

    if temperature is not None:
        for i in range(0,delay):
            y4.Display('{0:0.1f}C'.format(temperature)) 
            time.sleep(0.005)

    if humidity is not None: 
        for i in range(0,delay):
            y4.Display('H{0:0.1f}'.format(humidity)) 
            time.sleep(0.005)
    