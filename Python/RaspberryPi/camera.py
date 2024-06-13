from picamera import PiCamera
from time import sleep

if __name__ == '__main__':
    camera = PiCamera()
    camera.resolution = (2592, 1944)
    camera.framerate = 15
    camera.start_preview()
    sleep(3)
    camera.capture('/7tiny/test.jpg')
    camera.stop_preview()