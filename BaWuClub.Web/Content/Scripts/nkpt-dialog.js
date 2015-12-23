/*
nanking phoenix tree dialog
version 1.0.1
*/


var dialog=function(c){

	if(typeof c==='string'){
		var txt=c;
		c={}
		c.context=(c=='img'?"<img src='../images/load.gif'/>":txt);
		c.type='tips'
		c.lock=true
		c.time=2000
	}
	
	
	var config = {
		id: c.id,
		title: c.title,
		context: c.context,
		oktext: c.oktext,
		canceltext: c.canceltext,
		okfn: c.okfn,
		cancelfn: c.cancelfn,
		width: c.width,
		height: c.height,
		lock: (c.lock==undefined?true:c.lock),
		time: c.time,
		type:c.type
	}
	var s=init(config);
	
	var btnok=document.getElementById("dialog-ui-btn-ok")
	var btncancel=document.getElementById("dialog-ui-btn-cancel")
	
	if(btnok!=undefined){
		btnok.addEventListener("click",function(){
			config.okfn==undefined?s.remove():config.okfn
		})	
	}
	
	if(btncancel!=undefined){
		btncancel.addEventListener("click",function(){
		config.cancelfn==undefined?s.remove():config.cancelfn
		})
	}
		
	return s;
}

function init(config){
	var box=document.createElement("div")
	var mask=document.createElement("div")
	var buttonclose=document.createElement("div")
	mask.className='dialog-ui-mask'
	box.className='dialog-ui-wrap'
	box.setAttribute("id","dialog")	
	buttonclose.setAttribute("id","dialog-button-close")
	buttonclose.className="dialog-ui-close fright"
	
	var t={
		messagebox:function(){
			box.appendChild(buttonclose)
			var buttonclosehtml=box.innerHTML;
			var str='<div class="dialog-ui-header">'
				+'<div class="dialog-ui-title fleft">'
				+config.title+'</div>'
				+buttonclosehtml
				+'</div>'
				+'<div class="dialog-ui-context">'+config.context+'</div>'
				+'<div class="dialog-ui-btns">'
				+'<input name="dialog-ui-btn-ok" id="dialog-ui-btn-ok" type="button" value='+(config.oktext===undefined?"确定":config.oktext)+' class="dialog-ui-btn dialog-ui-btn-ok"/>'
				+'<input name="dialog-ui-btn-cancel" id="dialog-ui-btn-cancel" type="button" value='+(config.canceltext===undefined?"取消":config.canceltext)+' class="dialog-ui-btn dialog-ui-btn-cancel"/>'
				+'</div>'
			box.innerHTML=str
			document.body.appendChild(box)
			setwrapcenter(box)
			var eid=buttonclose.id
			document.getElementById(eid).addEventListener("click",function(){closedialog()})
		},
		tips:function(){
			var str='<div class="dialog-ui-context tips">'+config.context+'</div>'
				box.innerHTML=str
			document.body.appendChild(box)
			setwrapcenter(box)
			setTimeout(this.remove,config.time)
		},
		remove:function(){
			document.body.removeChild(box)
			if(mask!=null)
				document.body.removeChild(mask)
		}
	}
	
	
	mask.addEventListener("click",function(){closedialog()})
		
	function closedialog(){
		t.remove()
	}
	
	function setlock(){
		if(config.lock)
			document.body.appendChild(mask)
	}


	setlock()
	var u=config.type=='dialog'?t.messagebox():t.tips()
	
	var d={
		dialog:function(){u()},
		remove:function(){t.remove()}
	}
	
	return d;
}

function setwrapcenter(element){
	var width=element.offsetWidth
	var height=element.offsetHeight
	var top=(window.document.body.clientHeight-height)/2
	var left=(document.body.clientWidth-width)/2
	element.setAttribute("style","top:"+top+"px;left:"+left+"px")	
}



