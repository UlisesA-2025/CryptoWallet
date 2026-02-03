export function formatCrypto(code) {
  if (!code) return '';
  return String(code).toUpperCase();
}

export function formatAction(action) {
  if (action === 'purchase') return 'Compra';
  if (action === 'sale') return 'Venta';
  return action ?? '';
}

export function formatMoney(value) {
  if (value == null || value === '') return '0,00';

  return Number(value).toLocaleString('es-AR', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  });
}
