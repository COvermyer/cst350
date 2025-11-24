$(document).ready(function () { // cascading load trigger

    // preselections
    const selectedBook = parseInt($("#bookId").data("selected-book")) || 1;
    const selectedChapter = parseInt($("#chapterNumber").data("selected-chapter")) || 1;
    const selectedVerse = parseInt($("#verseNumber").data("selected-verse")) || 1;

    // Selectors
    const bookSelect = $(".bookId");
    const chapterSelect = $(".chapterNumber");
    const verseSelect = $(".verseNumber");

    // AJAX fetch API loads Books, triggers bookId onchange
    fetch(`/api/v1/Bible/books`)
        .then(res => res.json())
        .then(books => {
            bookSelect.html("");
            books.forEach(b => bookSelect.append(`<option value="${b.bookId}" ${b.bookId === selectedBook ? "selected" : ""}>${b.bookName}</option>`));
            bookSelect.trigger("change");
        });

    // AJAX fetch API loads chapters on bookId change, triggers chapterNumber onchange
    $(document).on("change", ".bookId", function (e) {
        e.preventDefault();

        const bookId = $(".bookId").val();
        fetch(`/api/v1/Bible/books/${bookId}/chapters`)
            .then(res => res.json())
            .then(chapters => {
                chapterSelect.html("");
                chapters.forEach(ch => chapterSelect.append(`<option value="${ch}" ${parseInt(ch) === selectedChapter ? "selected" : ""}>${ch}</option>`));
                chapterSelect.trigger("change");
            });
    });

    // AJAX fetch API loads verseNumber on chapterNumber change, triggers verseNumber onchange
    $(document).on("change", ".chapterNumber", function (e) {
        e.preventDefault();

        const bookId = $(".bookId").val();
        const chapterNumber = $(".chapterNumber").val();
        fetch(`/api/v1/Bible/books/${bookId}/chapters/${chapterNumber}/verses`)
            .then(res => res.json())
            .then(verses => {
                verseSelect.html("");
                verses.forEach(v => verseSelect.append(`<option value="${v}" ${parseInt(v) === selectedVerse ? "selected" : ""}>${v}</option>`));
                verseSelect.trigger("change");
            });
    });

    // AJAX fetch API changes verseText html on verseNumber change, dynamically loads content
    // This should also load any comments associated with the verse
    $(document).on("change", ".verseNumber", function (e) {
        e.preventDefault();

        const bookId = $(".bookId").val();
        const chapterNumber = $(".chapterNumber").val();
        const verseNumber = $(".verseNumber").val();
        fetch(`/api/v1/Bible/books/${bookId}/chapters/${chapterNumber}/verses/${verseNumber}`)
            .then(res => res.json())
            .then(data => {
                $("#verseText").html(`<strong>${data.bookName} ${data.chapter}:${data.verse}</strong><br>${data.text}`);
            });
    });
});
