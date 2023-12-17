function deleteMovie(movieId) {
    if (confirm('Are you sure you want to delete this movie?')) {
        fetch(`/Movies/Delete/${movieId}`, {
            method: 'POST'
        })
        .then(response => {
            if (response.ok) {
                location.reload();
            } else {
                alert('Error deleting movie');
            }
        })
        .catch(error => console.error('Error:', error));
    }
}
