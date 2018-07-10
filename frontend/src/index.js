import _ from 'lodash';
// import * as helpermodule from './helper.js' 
//add resolve is soltion for extension
import * as helpermodule from './helper'
import './sharedModule'

function component() {
    var element = document.createElement('div');
    
    // Lodash, currently included via a script, is required for this line to work
    element.innerHTML = _.join(['Hello', 'webpack'], ' ');
  
    return element;
  }
  
  document.body.appendChild(component());
  console.log("hello webpasasasack", helpermodule.greetings)

var arr=[ 1, 2, 3];
_.each(arr,function(val) {
  console.log('Output from Lodash _.each for Element ' + val); 
});