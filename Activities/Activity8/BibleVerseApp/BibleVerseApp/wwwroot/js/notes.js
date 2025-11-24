$(document).ready(function () {

    // AJAX call to load in Note text
    $(document).on("change", ".verseNumber", function (e) {
        e.preventDefault();

        const bookId = $(".bookId").val();
        const chapterNumber = $(".chapterNumber").val();
        const verseNumber = $(".verseNumber").val();
        fetch(`/api/v1/Note/${bookId}/${chapterNumber}/${verseNumber}`)
            .then(res => {
                if (!res.ok) throw new Error(`HTTP error! status: ${res.status}`);
                return res.text().then(text => text ? JSON.parse(text) : {});
            })
            .then(data => {
                const noteArea = $(".note-area");
                noteArea.val(data.noteText);
                console.log(data.noteText);
            });
    });

    // Save Note API call
    $("#save-note").on("click", function (e) {
        e.preventDefault();

        const noteData = {
            bookId: parseInt($(".bookId").val()),
            chapterNumber: parseInt($(".chapterNumber").val()),
            verseNumber: parseInt($(".verseNumber").val()),
            noteText: $(".note-area").val()
        };
        fetch("/api/v1/Note/SaveNote", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(noteData)
        })
            .then(res => {
                if (!res.ok) throw new Error(`HTTP error! status: ${res.status}`);
                return res.text().then(text => text ? JSON.parse(text) : {});
            })
            .then(data => {
                console.log("Saved Note: ", data);
                alert("Note Saved!");
            })
            .catch(err => {
                console.error("Error saving note: ", err);
                alert("Failed to save note.");
            })
    });

    $("#delete-note").on("click", function (e) {
        e.preventDefault();

        const bookId = $(".bookId").val();
        const chapterNumber = $(".chapterNumber").val();
        const verseNumber = $(".verseNumber").val();
        if (!confirm("Are you sure you want to delete this note?")) return;

        fetch(`/api/v1/Note/${bookId}/${chapterNumber}/${verseNumber}`, {
            method: "DELETE"
        })
            .then(res => {
                if (!res.ok) throw new Error(`HTTP error! status: ${res.status}`);
                return res.json();
            })
            .then(data => {
                console.log("Deleted Note: ", data);
                $(".note-area").val(""); // clear text area
                alert(data.message);
            })
            .catch(err => {
                console.error("Error deleting note: ", err);
                alert("Failed to delete note.");
            });
    })
});