
function successCallback(result)
{
    for (var i = 0; i < result.entities.length; i++)
    {
        console.log(result.entities[i]);
    }    
}

function errorCallback(error)
{
    console.error(error.message);
}

function form_onLoad(ex_context)
{
    alert('Hi I was successful');
    Xrm.Navigation.openAlertDialog('Xrm I was successful');

    var success = successCallback;
    var error = errorCallback;

    Xrm.WebApi.retrieveMultipleRecords("contact", "?$select=firstname,lastname").then(success, error);
    
}
