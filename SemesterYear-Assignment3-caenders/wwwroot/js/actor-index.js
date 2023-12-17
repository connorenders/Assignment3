function deleteActor(actorId) {
    if (confirm('Are you sure you want to delete this actor?')) {
        fetch(`/Actors/Delete/${actorId}`, {
            method: 'POST'
        })
            .then(response => {
                if (response.ok) {
                    location.reload();
                } else {
                    alert('Error deleting actor');
                }
            })
            .catch(error => console.error('Error:', error));
    }
}
