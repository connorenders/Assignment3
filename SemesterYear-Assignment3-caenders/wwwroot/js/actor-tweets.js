document.addEventListener("DOMContentLoaded", function () {
    const actorIdElement = document.getElementById("actorId");
    const actorId = actorIdElement.getAttribute("data-actor-id");
    let count = 0;
    let totalSentiment = 0;

    function fetchReview() {
        fetch(`/Actors/GenerateTweet/${actorId}`)
            .then(response => response.json())
            .then(data => {
                addReviewToTable(data.tweet, data.sentiment);
                totalSentiment += data.sentiment;
                count++;

                if (count < 20) {
                    fetchReview(); // Fetch again if not yet run 10 times
                } else {
                    // Calculate and display the average sentiment
                    const averageSentiment = totalSentiment / 20;
                    const overallSentimentElement = document.getElementById("overallSentiment");
                    overallSentimentElement.textContent = `Overall Sentiment: ${averageSentiment.toFixed(2)}`;

                    const waitLabelElement = document.getElementById("wait_lbl");
                    waitLabelElement.textContent = ``;
                }
            });
    }

    fetchReview(); // Start fetching immediately
});

function addReviewToTable(tweet, sentiment) {
    const table = document.getElementById("reviewsTable").getElementsByTagName("tbody")[0];
    const row = table.insertRow();
    row.insertCell(0).textContent = tweet;
    row.insertCell(1).textContent = sentiment;
}
