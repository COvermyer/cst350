$(document).ready(function () {

    let currentPage = 1;
    const pageSize = 10;

    $("#search-form").on("submit", function (e) {
        e.preventDefault();
        currentPage = 1;
        runSearch();
    })

    function runSearch() {
        const query = $("#search-query").val().trim();
        const inOT = $("#inOldTestament").is(":checked");
        const inNT = $("#inNewTestament").is(":checked");
        if (!query) return alert("Please enter a search term.");

        const url = `/api/v1/Bible/search?query=${encodeURIComponent(query)}`
            + `&inOldTestament=${inOT}`
            + `&inNewTestament=${inNT}`
            + `&page=${currentPage}`
            + `&pageSize=${pageSize}`;

        fetch(url)
            .then(res => {
                if (!res.ok) {
                    if (res.status === 404) {
                        return [];
                    }
                    throw new Error("Search failed");
                }
                return res.json();
            })
            .then(data => {
                renderResults(data.results);
                renderPagination(data.totalResults, data.page, data.pageSize);
            })
            .catch(err => {
                console.error(err);
                $("#search-results ul").html("<li class='list-group-item text-danger'>Error during search.</li>");
                $("#pagination-container").html("");
            });
    }

    function renderResults(results) {
        if (!results || results.length === 0) { // Handle 0 match case
            $("#search-results ul").html("<li class='list-group-item'>No results found.</li>");
            return;
        }

        let html = "";
        results.forEach(v => {
            html += `
                 <li class="list-group-item search-result-item"
                    data-book="${v.bookId}" 
                    data-chapter="${v.chapterNumber}" 
                    data-verse="${v.verseNumber}">
                    <a href="/Bible/NotateVerse?bookId=${v.bookId}&chapterNumber=${v.chapterNumber}&verseNumber=${v.verseNumber}" 
                       class="text-decoration-none fw-bold" target="_blank" >
                        <strong>${v.bookName} ${v.chapterNumber}:${v.verseNumber}</strong>
                    </a><br>
                    <span>${v.verseText}</span>
                 </li>
            `
        });
        $("#search-results ul").html(html);
    }

    function renderPagination(total, page, size) {
        const totalPages = Math.ceil(total / size);

        if (totalPages <= 1) {
            $("#search-results-total").html("");
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

        $("#search-results-total").html(`Total Results: ${total}`);
        $("#pagination-container").html(html);
    }

    // Pagination click handler for dynamically handled pagination links
    $("#pagination-container").on("click", ".page-link", function (e) {
        e.preventDefault();
        const newPage = parseInt($(this).data("page"));
        if (!newPage || newPage < 1) return;
        currentPage = newPage;
        runSearch();
        window.scrollTo(0, 0);
    });
})
