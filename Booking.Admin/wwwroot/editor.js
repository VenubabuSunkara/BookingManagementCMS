tinymce.init({
    selector: "textarea.descriptionOf", // Keep for backward compatibility if needed
    width: 'auto',
    license_key: 'gpl',
    height: 300,
    plugins: [
        "advlist",
        "autolink",
        "link",
        "image",
        "lists",
        "charmap",
        "preview",
        "anchor",
        "pagebreak",
        "searchreplace",
        "wordcount",
        "visualblocks",
        "code",
        "fullscreen",
        "insertdatetime",
        "media",
        "table",
        "emoticons",
        "codesample",
    ],
    toolbar:
        "undo redo | styles | bold italic underline | alignleft aligncenter alignright alignjustify |" +
        "bullist numlist outdent indent | link image | print preview media fullscreen | " +
        "forecolor backcolor emoticons",
    menu: {
        favs: {
            title: "menu",
            items: "code visualaid | searchreplace | emoticons",
        },
    },
    menubar: "favs file edit view insert format tools table",
    content_style: "body{font-family:Helvetica,Arial,sans-serif; font-size:16px}",
    target: document.querySelector('.descriptionOf')
});
