$(document).ready(function () {

    // selectors
    const bookSelect = $("#bookId");
    const chapterSelect = $("#chapterNumber");

    // containers
    const chapterContainer = $("#chapterVerses");
    const paginationContainer = $("#pagination-container");

    let currentPage = 1;
    const pageSize = 10;

    // load books
    fetch(`/api/v1/Bible/books`)
        .then(res => res.json())
        .then(books => {
            bookSelect.html("");
            books.forEach(b => bookSelect.append(`<option value="${b.bookId}">${b.bookName}</option>`));
            bookSelect.trigger("change");
        });

    // load chapters when book changes
    $(document).on("change", "#bookId", function (e) {
        e.preventDefault();
        const bookId = $(this).val();
        fetch(`/api/v1/Bible/books/${bookId}/chapters`)
            .then(res => res.json())
            .then(chapters => {
                chapterSelect.html("");
                chapters.forEach(ch => chapterSelect.append(`<option value="${ch}">${ch}</option>`));
                chapterSelect.trigger("change");
            });
    });

    // load verses when chapter changes
    $(document).on("change", "#chapterNumber", function () {
        currentPage = 1;
        loadVerses();
    });

    function loadVerses() {
        const bookId = bookSelect.val();
        const chapterNumber = chapterSelect.val();
        fetch(`/api/v1/Bible/books/${bookId}/chapters/${chapterNumber}/allverses?page=${currentPage}&pageSize=${pageSize}`)
            .then(res => res.json())
            .then(data => {
                renderVerses(data.results);
                renderPagination(data.totalResults, currentPage, pageSize)
            });
    }

    function renderVerses(verses) {
        chapterContainer.html("");
        if (!verses || verses.length === 0) {
            chapterContainer.html("<li class='list-group-item text-muted'>No verses found.</li>");
            return;
        }

        verses.forEach(v => {
            chapterContainer.append(`
                <li class="list-group-item">
                    <a href="/Bible/NotateVerse?bookId=${v.bookId}&chapterNumber=${v.chapterNumber}&verseNumber=${v.verseNumber}" 
                       class="text-decoration-none fw-bold" target="_blank">
                        <strong>${v.bookName} ${v.chapterNumber}:${v.verseNumber}</strong>
                    </a>
                    <p class="mb-0">${v.verseText}</p>
                </li>
            `);
        })
    }

    function renderPagination(total, page, size) {
        const totalPages = Math.ceil(total / size);

        if (totalPages <= 1) {
            $("#pagination-container").html("");
            return;
        }
        const maxButtons = 10;
        let startPage = Math.max(1, page - Math.floor(maxButtons / 2));
        let endPage = startPage + maxButtons - 1;

        if (endPage > totalPages) {
            endPage = totalPages;
            startPage = Math.max(1, endPage - maxButtons + 1);
        }


        let html = `<nav><ul class="pagination justify-content-center">`; // opening tage

        // Previous link in pagination
        html += `
            <li class="page-item ${page === 1 ? "disabled" : ""}">
                <a class="page-link" href="#" data-page="${page - 1}">Previous</a>
            </li>
        `;

        // List of pages
        for (let i = startPage; i <= endPage; i++) {
            html += `
                <li class="page-item ${i === page ? "active" : ""}">
                    <a class="page-link" href="#" data-page="${i}">${i}</a>
                </li>
            `;
        }

        // Next link in pagination
        html += `
            <li class="page-item ${page === totalPages ? "disabled" : ""}">
                <a class="page-link" href="#" data-page="${page + 1}">Next</a>
            </li>
        `;

        html += `</ul></nav>`; // closing tag
        $("#pagination-container").html(html);
    }

    $("#pagination-container").on("click", ".page-link", function (e) {
        e.preventDefault();
        const newPage = parseInt($(this).data("page"));
        if (!newPage || newPage < 1) return;
        currentPage = newPage;
        loadVerses();
        window.scrollTo(0, 0);
    })
});