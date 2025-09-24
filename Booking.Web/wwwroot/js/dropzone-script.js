var DropzoneExample = function () {
    var DropzoneDemos = function () {
        Dropzone.options.singleFileUpload = {
            url: "/Packages/Single",
            paramName: "file",
            maxFiles: 1,
            maxFilesize: 2,
            acceptedFiles: ".jpg,.jpeg,.png,.gif",
            dictDefaultMessage: "Drop files here or click to upload",
            init: function () {
                this.on("success", function (file, response) {
                    if (response.success) {
                        response.files.forEach(function (fileInfo) {
                            // Add preview
                            var previewHtml = `
                        <div class="col-md-3 mb-3">
                            <div class="card">
                                <img src="${fileInfo.thumbnailPath}" class="card-img-top" alt="${fileInfo.originalFileName}">
                                <div class="card-body">
                                    <p class="card-text">${fileInfo.originalFileName}</p>
                                    <small class="text-muted">${(fileInfo.fileSize / 1024).toFixed(2)} KB</small>
                                </div>
                            </div>
                        </div>`;
                            $("#file-preview-single").append(previewHtml);
                        });
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
                        response.files.forEach(function (fileInfo) {
                            // Add preview
                            var previewHtml = `
                        <div class="col-md-3 mb-3">
                            <div class="card">
                                <img src="${fileInfo.thumbnailPath}" class="card-img-top" alt="${fileInfo.originalFileName}">
                                <div class="card-body">
                                    <p class="card-text">${fileInfo.originalFileName}</p>
                                    <small class="text-muted">${(fileInfo.fileSize / 1024).toFixed(2)} KB</small>
                                </div>
                            </div>
                        </div>
                    `;
                            $("#file-preview-multiple").append(previewHtml);
                        });
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