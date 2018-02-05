var isAwaitingUpdate = true;

function setAwaiter(value) {
	isAwaitingUpdate = value;
}

function beginSetup() {
	if (isAwaitingUpdate) {
		//return;
	}

	//window.location.href = "http://StartPage?command=base.html";
	window.location.href="base.html";
}

function submitRelaxationData()
{
	
}