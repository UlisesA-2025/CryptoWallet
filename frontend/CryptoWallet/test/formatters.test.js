import { describe, it, expect } from 'vitest';
import { formatMoney, formatAction, formatCrypto } from '../src/utils/formatters';

describe('formatters', () => {
  it('formatCrypto pone en mayúsculas', () => {
    expect(formatCrypto('btc')).toBe('BTC');
    expect(formatCrypto('Eth')).toBe('ETH');
  });

  it('formatAction traduce purchase/sale a español', () => {
    expect(formatAction('purchase')).toBe('Compra');
    expect(formatAction('sale')).toBe('Venta');
  });

  it('formatMoney formatea con 2 decimales (AR)', () => {
    expect(formatMoney(1234.5)).toBe('1.234,50');
    expect(formatMoney(697290.84845)).toBe('697.290,85');
  });

  it('formatMoney maneja null/undefined', () => {
    expect(formatMoney(null)).toBe('0,00');
    expect(formatMoney(undefined)).toBe('0,00');
  });
});
