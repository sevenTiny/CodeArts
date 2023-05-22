# coding=utf-8
import time
import RPi.GPIO as GPIO
import sys
sys.path.append('..')

reload(sys)
sys.setdefaultencoding('utf8')

import Adafruit_DHT
  
# Set sensor type : Options are DHT11,DHT22 or AM2302
sensor=Adafruit_DHT.DHT11
  
# Set GPIO sensor is connected to
# this is bcm code
gpio=4

for i in range(0,20):
    # Use read_retry method. This will retry up to 15 times to
    # get a sensor reading (waiting 2 seconds between each retry).
    # this is bcm code
    humidity, temperature = Adafruit_DHT.read_retry(Adafruit_DHT.DHT11, 4)
    
    # Reading the DHT11 is very sensitive to timings and occasionally
    # the Pi might fail to get a valid reading. So check if readings are valid.
    if humidity is not None and temperature is not None:
        print('Temp={0:0.1f}*C  Humidity={1:0.1f}%'.format(temperature, humidity))
    else:
        print('Failed to get reading. Try again!')
    time.sleep(3)