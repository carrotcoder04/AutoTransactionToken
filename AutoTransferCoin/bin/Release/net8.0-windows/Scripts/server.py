import ctypes
import os
import re
import time
from threading import Thread
import xml.etree.ElementTree as ET
from flask import Flask, jsonify, g, request
import ui

os.system("title Server") 
app = Flask(__name__)
def extract_content_desc(root,element):
    data = []
    content_desc = root.get(element)
    if content_desc:
        data.append(content_desc)
    for child in root:
        data.extend(extract_content_desc(child,element))
    return data
@app.route('/ce', methods=['POST'])
def click_element():
    data = request.get_json()
    print(data)
    index = data['index']
    element = data['element']
    a = ui.get_device(index).device(descriptionContains = element)
    if a.exists:
        a.click()
        return "OK"
    else:
        return "FAIL"
@app.route('/ce3', methods=['POST'])
def click_element3():
    data = request.get_json()
    print(data)
    index = data['index']
    element_index = data['element_index']
    element = data['element']
    elements = ui.get_device(index).device(descriptionContains=element)
    if element_index >= 0 and element_index < len(elements):
        element = elements[element_index]
        element.click()
        return "OK"
    else:
        return "FAIL"
@app.route('/ct', methods=['POST'])
def contain():
    data = request.get_json()
    print(data)
    index = data['index']
    element = data['element']
    a = ui.get_device(index).device(descriptionContains = element)
    if a.exists:
        return "OK"
    else:
        return "FAIL"
@app.route('/ce2', methods=['POST'])
def click_element2():
    data = request.get_json()
    print(data)
    index = data['index']
    element = data['element']
    a = ui.get_device(index).device(description = element)
    if a.exists:
        a.click()
        return "OK"
    else:
        return "FAIL"
@app.route('/it', methods=['POST'])
def set_text_element():
    data = request.get_json()
    print(data)
    index = data['index']
    text = data['text']
    a = ui.get_device(index).device(className = 'android.widget.EditText')
    if a.exists:
        a.click()
        time.sleep(0.15)
        a.set_text(text=text)
        return "OK"
    else:
        return "FAIL"
@app.route('/it2', methods=['POST'])
def set_text_element2():
    data = request.get_json()
    print(data)
    index = data['index']
    element_index = data['element_index']
    text = data['text']
    elements = ui.get_device(index).device(className = 'android.widget.EditText')
    if element_index >= 0 and element_index < len(elements):
        element = elements[element_index]
        element.click()
        time.sleep(0.15)
        element.set_text(text=text)
        return "OK"
    else:
        return "FAIL"

@app.route('/dmp/<index>', methods=['GET'])
def dump(index):
    index = int(index)
    return ui.get_device(index).device.dump_hierarchy()

@app.route('/key/<index>', methods=['GET'])
def get_key_register(index):
    index = int(index)
    device = ui.get_device(index).device
    btn = device(descriptionContains = 'btn_press_and_hold')
    
    while True:
        btn.long_click(duration=0.6)
        xml =  device.dump_hierarchy()
        root = ET.fromstring(xml)
        data = extract_content_desc(root,'content-desc')
        pattern = r"^\d+\n"
        matches = [desc.replace('\n',' ') for desc in data if re.match(pattern, desc)]
        sorted_data = sorted(matches, key=lambda x: int(x.split()[0]))
        sorted_data = [x.split()[1] for x in sorted_data]
        if(len(sorted_data) > 0):
            if(sorted_data[0][0] =='-'):
                print('-')
                continue
            return ' '.join(sorted_data)

@app.route('/address/<index>', methods=['GET'])
def get_address(index):
    index = int(index)
    device = ui.get_device(index)
    elements = device.device(className = 'android.widget.Button')
    content_descs = [elem.info.get("contentDescription", "") for elem in elements if
                     elem.info.get("contentDescription")]
    result =  content_descs[0].split()[0]
    if result[0] != 's':
        return 'FAIL'
    return result
app.run(port=9123)
