
// run the indexer here as a console application or a scheduled cron job
// the indexer will index the data from the tvmaze api using the tvmaze gateway, it is then supposed to call the repository to save the data.
// the repository is not implemented yet, so the indexer will not be able to save the data.

// While the indexer is running, the idea is to go through the api, upping the page from https://api.tvmaze.com/shows