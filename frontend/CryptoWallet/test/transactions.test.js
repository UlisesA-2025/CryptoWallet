import { describe, it, expect } from 'vitest';
import { getVisibleTransactions } from '../src/utils/transactions';

describe('transactions utils', () => {
  it('si showAll=false muestra solo 5', () => {
    const txs = Array.from({ length: 10 }, (_, i) => ({ id: i + 1 }));
    const visible = getVisibleTransactions(txs, false, 5);
    expect(visible).toHaveLength(5);
    expect(visible[0].id).toBe(1);
    expect(visible[4].id).toBe(5);
  });

  it('si showAll=true muestra todas', () => {
    const txs = Array.from({ length: 10 }, (_, i) => ({ id: i + 1 }));
    const visible = getVisibleTransactions(txs, true, 5);
    expect(visible).toHaveLength(10);
  });

  it('si no hay array devuelve []', () => {
    expect(getVisibleTransactions(null, false)).toEqual([]);
  });
});
