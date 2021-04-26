async function sayHello(dotnetHelper, numberofRuns) {
    for (var i = 0; i < numberofRuns; i++) {
        console.log("Start");
        await dotnetHelper.invokeMethodAsync('RunTest');
        console.log("End");
    }
    console.log("Finish");
    await dotnetHelper.invokeMethodAsync('Finish');

    dotnetHelper.dispose();

}

function saveAsFile(filename, bytesBase64) {
        var link = document.createElement('a');
        link.download = filename;
        link.href = "data:application/octet-stream;base64," + bytesBase64;
        document.body.appendChild(link); // Needed for Firefox
        link.click();
        document.body.removeChild(link);
    }



  function returnArrayAsyncJs() {

 DotNet.invokeMethodAsync('BlazorClientBenchmark', 'RunBinaryTreeTest');

  
};


function repeat(func, times, dotnetHelper) {
    var promise = Promise.resolve();
    while (times-- > 0) promise = promise.then(func, dotnetHelper);
    return promise;
}

function oneSecond() {
    return new Promise(function (resolve, reject) {
        dotnetHelper.invokeMethodAsync('RunTest');
        setTimeout(function () {
            console.log("Waiting 5 seconds");
            resolve();
        }, 5000);
    });
}

