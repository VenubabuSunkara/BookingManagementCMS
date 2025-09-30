var DropzoneExample = function (current) {
    debugger;
    var DropzoneDemos = function (current) {
        debugger;
        const a = $(current);
        console.log(current);
        debugger;
        Dropzone.options.singleFileUpload = {
            url: "/Packages/Single",
            paramName: "file",
            maxFiles: 1,
            maxFilesize: 2,
            acceptedFiles: ".jpg,.jpeg,.png,.gif",
            dictDefaultMessage: "Drop files here or click to upload",
            init: function () {
                this.on("success", function (file, response) {
                    debugger;
                    if (response.success) {
                        $("#SingleMediajson").val(JSON.stringify(response.files));
                    } else {
                        alert(response.message);
                    }
                });

                this.on("error", function (file, response) {
                    alert("Error uploading file: " + response.message);
                });
            }
        };
        Dropzone.options.multiFileUpload = {
            url: "/Packages/Multiple",
            paramName: "files",
            maxFiles: 10,
            maxFilesize: 20,
            acceptedFiles: ".jpg,.jpeg,.png,.gif",
            dictDefaultMessage: "Drop files here or click to upload",
            init: function () {
                this.on("success", function (file, response) {
                    if (response.success) {
                        $("#MultipleMediajson").val(JSON.stringify(response.files));
                    } else {
                        alert(response.message);
                    }
                });

                this.on("error", function (file, response) {
                    alert("Error uploading file: " + response.message);
                });
            }
        };
        Dropzone.options.fileTypeValidation = {
            paramName: "file",
            maxFiles: 10,
            maxFilesize: 10,
            acceptedFiles: "image/*,application/pdf,.psd",
            accept: function (file, done) {
                if (file.name == "justinbieber.jpg") {
                    done("Naha, you don't.");
                } else {
                    done();
                }
            }
        };
    }
    return {
        init: function () {
            DropzoneDemos();
        }
    };
}();
DropzoneExample.init();