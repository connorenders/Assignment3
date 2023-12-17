document.addEventListener("DOMContentLoaded", function () {
    const movieIdElement = document.getElementById("movieId");
    const movieId = movieIdElement.getAttribute("data-movie-id");
    let count = 0;
    let totalSentiment = 0;

    function fetchReview() {
        fetch(`/Movies/GenerateReview/${movieId}`)
            .then(response => response.json())
            .then(data => {
                addReviewToTable(data.review, data.sentiment);
                totalSentiment += data.sentiment;
                count++;

                if (count < 10) {
                    fetchReview(); // Fetch again if not yet run 10 times
                } else {
                    // Calculate and display the average sentiment
                    const averageSentiment = totalSentiment / 10;
                    const overallSentimentElement = document.getElementById("overallSentiment");
                    overallSentimentElement.textContent = `Overall Sentiment: ${averageSentiment.toFixed(2)}`;
                    
                    const waitLabelElement = document.getElementById("wait_lbl");
                    waitLabelElement.textContent = ``;
                }
            });
    }

    fetchReview(); // Start fetching immediately
});

function addReviewToTable(review, sentiment) {
    const table = document.getElementById("reviewsTable").getElementsByTagName("tbody")[0];
    const row = table.insertRow();
    row.insertCell(0).textContent = review;
    row.insertCell(1).textContent = sentiment;
}
