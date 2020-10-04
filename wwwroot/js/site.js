// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#inputFile').on('change', function (evt) {
    // get the file name
    var fileName = document.getElementById("inputFile").files[0].name;
    // replace the "Choose a file" label
    $(this).next('#inputFileLabel').html(fileName);


    // Generate presigned url
    $.ajax('home/GeneratePresignedUrl', {
        data: { "fileName": fileName },  // data to submit
        type: 'POST',
        dataType: 'json',
        success: function (data, status, xhr) {
            if (data !== null) {
                // Parse data from string to json
                data = $.parseJSON(data);
                $('#ResultPresignedS3Url').val(data.PresignedUrl);
                $('#presignedUrlToDisplay').innerText = data.PresignedUrl;
                $('#ResultFilename').val(data.FileName);
                // if presigned url generated okay then show the save button
                $('#saveVideoButton').removeAttr("disabled");
            }
        },
        error: function (jqXhr, textStatus, errorMessage) {
            alert("Error generating filename");
        }
    });

});

// helper functions for upload handler
function UploadProgress(evt) {
    if (evt.lengthComputable) {
        var percentComplete = Math.round(evt.loaded * 100 / evt.total);
        document.getElementById('progressNumber').innerHTML = percentComplete.toString() + '%';
    }
    else {
        document.getElementById('progressNumber').innerHTML = 'unable to compute';
    }
}

$("#saveVideoButton").click(function () {
    // upload handler
    function uploadFile() {
        var file = document.getElementById('inputFile').files[0];
        var contentType = file.type;
        var presignedUrl = $('#ResultPresignedS3Url').val()

        $.ajax({
            type: 'PUT',
            url: presignedUrl,
            // Content type must much with the parameter you signed your URL with
            contentType: contentType,
            // this flag is important, if not set, it will try to send data as a form
            processData: false,
            // the actual file is sent
            data: file,
            success: function (data, status, xhr) {
                alert("Upload success");
            },
            error: function (error) {
                alert('File NOT uploaded' + error);
            },
            // Monitoring of progress
            xhr: function () {
                var xhr = new window.XMLHttpRequest();
                xhr.upload.addEventListener("progress", function (evt) {
                    if (evt.lengthComputable) {
                        UploadProgress(evt);
                        //Do something with upload progress here
                    }
                }, false);
                return xhr;
            },
        });
    }
    // When button pressed
    uploadFile();
});

