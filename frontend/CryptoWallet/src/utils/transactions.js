export function getVisibleTransactions(allTxs, showAll, limit = 5) {
  if (!Array.isArray(allTxs)) return [];
  return showAll ? allTxs : allTxs.slice(0, limit);
}
