import time

from flask import Flask, jsonify, g, request

import cmd
import config
import ui

app = Flask(__name__)

@app.route('/click_element/<index>/<element>', methods=['GET'])
def get_data(index,element):
    index = int(index)
    a = ui.get_device(index).device(descriptionContains = element)
    if a.exists:
        a.click()
        return "OK"
    else:
        return "FAIL"

app.run(port=9123)