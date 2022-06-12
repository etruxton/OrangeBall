# Python program for Detection of a
# specific color(blue here) using OpenCV with Python
import cv2
import numpy as np
import socket
import time
import math

UDP_IP = "127.0.0.1"
UDP_PORT = 5065

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

last = []
 
# Webcamera no 0 is used to capture the frames
cap = cv2.VideoCapture(0)
cX, cY = 0, 0
#cap = cv2.VideoCapture('movie1.mp4')
# This drives the program into an infinite loop.
while(1):       
    # Captures the live stream frame-by-frame
    _, frame = cap.read()
    frame = cv2.flip(frame, 1)
    # Converts images from BGR to HSV
    #dim = (600, 480)
    dim = (550,750)
    frame = cv2.resize(frame, dim, interpolation = cv2.INTER_AREA)
    hsv = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)
    #lower_orange = np.array([0,145,120])
    #upper_orange = np.array([13,255,255])

    #lower_orange = np.array([96,170,0])
    #upper_orange = np.array([179,255,255])
    #old computer^^
    
    lower_orange = np.array([0, 129, 167])
    upper_orange = np.array([24, 165, 255])
 
    # Here we are defining range of bluecolor in HSV
    # This creates a mask of blue coloured
    # objects found in the frame.
    mask = cv2.inRange(hsv, lower_orange, upper_orange)
 
    # The bitwise and of the frame and mask is done so
    # that only the blue coloured objects are highlighted
    # and stored in res
    res = cv2.bitwise_and(frame,frame, mask= mask)
    ###
    kernel = 5
    blurred_mask = cv2.GaussianBlur(mask, (kernel, kernel), 0)
    ###

    #ret,thresh = cv2.cv2.threshold(mask, 127,255, 0)
    M= cv2.moments(mask)

    if M["m00"]!=0:
        cX = int(M["m10"] / M["m00"])
        cY = int(M["m01"] / M["m00"])
        
    cv2.circle(res, (cX, cY), 5, (255, 255, 255), -1)
    #cv2.putText(res, "dot!", (cX - 25, cY - 25),cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 255, 255), 2)
    
    cv2.imshow('frame',frame)
#    cv2.imshow('mask',mask)
    cv2.imshow('res',res)

    c = cX, cY
    sock.sendto( str(c).encode(), (UDP_IP, UDP_PORT) )
    #sock.sendto( str(cY).encode(), (UDP_IP, UDP_PORT) )
    #cv2.imshow('blurred mask',blurred_mask)
 
    # This displays the frame, mask
    # and res which we created in 3 separate windows.
    k = cv2.waitKey(5) & 0xFF
    if k == 27:
        print("Closing Program...")
        break
    elif k == ord('s'):
        print(cX,cY)
 
# Destroys all of the HighGUI windows.
cv2.destroyAllWindows()
 
# release the captured frame
cap.release()
