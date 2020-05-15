namespace Terraria
{
	public interface Interfaceable
	{
		bool DropSlot(int slotNum);

		void ButtonClicked(int buttonNum);

		bool CanPlaceSlot(int slotNum, Item mouseItem);

		void PlaceSlot(int slotNum);
	}
}
