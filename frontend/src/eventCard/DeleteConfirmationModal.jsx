import React, { useState } from "react";

const DeleteConfirmationModal = ({ isOpen, onConfirm, onCancel }) => {
  const [isDeleting, setIsDeleting] = useState(false);

  if (!isOpen) return null;

  const handleDeleteClick = async () => {
    if (isDeleting) return; // защита от повторного клика
    setIsDeleting(true);
    await onConfirm();
  };

  return (
    <div className="modal-overlay">
      <div className="modal-box">
        <h2>Подтверждение удаления</h2>
        <p>Вы действительно хотите удалить это мероприятие?</p>
        <div className="modal-buttons">
          <button
            className="delete"
            onClick={handleDeleteClick}
            disabled={isDeleting}
          >
            {isDeleting ? "Удаление..." : "Удалить"}
          </button>
          <button
            className="cancel"
            onClick={onCancel}
            disabled={isDeleting} // чтобы во время удаления нельзя было отменить
          >
            Отмена
          </button>
        </div>
      </div>
    </div>
  );
};

export default DeleteConfirmationModal;
