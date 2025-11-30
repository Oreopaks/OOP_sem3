using InventorySystem.Interfaces;
using InventorySystem.Models;
using System;

namespace InventorySystem.Strategies
{
    public class ConsumePotionStrategy : IItemUsageStrategy
    {
        public void Use(Item item, PlayerContext context)
        {
            if (item is Potion potion && potion.IsConsumable)
            {
                // При использовании зелья применяем эффект
                ApplyEffect(potion, context);
                
                // Зелье расходуется
                // В случае с ItemStack, количество будет уменьшено в InventoryService
            }
        }
        
        private void ApplyEffect(Potion potion, PlayerContext context)
        {
            switch (potion.Effect)
            {
                case EffectType.Heal:
                    context.Health += potion.Potency;
                    break;
                case EffectType.ManaRestore:
                    context.Mana += potion.Potency;
                    break;
                case EffectType.StrengthBoost:
                    context.Strength += potion.Potency;
                    break;
                case EffectType.SpeedBoost:
                    // Эффект скорости может быть реализован в игровом движке
                    break;
                case EffectType.Invisibility:
                    // Эффект невидимости может быть реализован в игровом движке
                    break;
            }
        }
    }
}