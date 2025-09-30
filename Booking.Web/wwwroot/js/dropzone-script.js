var DropzoneExample = function () {
    var getUploadUrl = function (element, defaultUrl) {
        // Try to get URL from data-upload-url attribute, fallback to defaultUrl
        var urls = $(element).data('upload-url') || defaultUrl;
        return $(element).data('upload-url') || defaultUrl;
    };
    var Jsonid = "";
    var outputId = function (element) {
        // Try to get URL from data-upload-url attribute, fallback to defaultUrl
        return $(element).data('ouput-json');
    };
    var DropzoneDemos = function () {

        Dropzone.options.singleFileUpload = {
            url: function () {
                // 'this.element' refers to the dropzone element
                Jsonid = outputId(this.element);
                return getUploadUrl(this.element, "/Packages/Single");
            },
            paramName: "file",
            maxFiles: 1,
            maxFilesize: 2,
            acceptedFiles: ".jpg,.jpeg,.png,.gif",
            dictDefaultMessage: "Drop files here or click to upload",
            init: function () {
                this.on("success", function (file, response) {
                    if (response.success) {
                        $("#" + Jsonid).val(JSON.stringify(response.files));
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
            url: function () {
                Jsonid = outputId(this.element);
                return getUploadUrl(this.element, "/Packages/Multiple");
            },
            paramName: "files",
            maxFiles: 10,
            maxFilesize: 20,
            acceptedFiles: ".jpg,.jpeg,.png,.gif",
            dictDefaultMessage: "Drop files here or click to upload",
            init: function () {
                this.on("success", function (file, response) {
                    if (response.success) {
                        $("#" + Jsonid).val(JSON.stringify(response.files));
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
            url: function () {
                return getUploadUrl(this.element, "/Packages/FileType");
            },
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