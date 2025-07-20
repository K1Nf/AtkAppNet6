import { useEffect, useState } from "react";
import DeleteConfirmationModal from '../eventCard/DeleteConfirmationModal';
import toastr from "toastr";
import "toastr/build/toastr.min.css";

const EventCard = () => {
  // Модальное окно и логика удаления
  const [showModal, setShowModal] = useState(false);

  const handleDelete = async () => {

    let pathToDelete = "/api/ref/events/delete/" + data.id;
    try {
      const response = await fetch(pathToDelete, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json"
        }
      });

      if (!response.ok) {
        const errorText = await response.text(); // получаем описание ошибки с сервера
        toastr.error(`Ошибка при удалении: ${errorText}. Попробуйте позже`, "Ошибка");
        return; // прерываем выполнение, чтобы не шло дальше
      }

      if (response.status == 204) {

        // сделать кнопки disabled (не доступными для клика)

        const data = await response.text();

        //  Показать уведомление
        toastr.success("Мероприятие успешно удалено из системы!", "Удалено");

        setTimeout(() => {
          window.location.href = "/events";
        }, 3000);
      }

    } catch (error) {

      // ОБРАБОТКА ОШИБКА *error*
      toastr.error("Произошла системная ошибка. Попробуйте позже.", "Ошибка");
    }

    setShowModal(false);
  };


  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // 1. Получаем текущий URL
  const path = window.location.pathname;  // Например: "/element/5"
  const parts = path.split("/");          // Разделяем строку по "/"
  const id = parts[parts.length - 1];     // Берем последний элемент (5)

  const backUrl = `/api/ref/events/${id}`;


  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(backUrl);
        if (!response.ok) {
          const errorText = await response.text(); // получаем описание ошибки с сервера

          alert(`Ошибка: ${errorText}`);
          window.history.go(-1);

          return;
        }
        else {
          const result = await response.json();
          setData(result);
        }
      } catch (err) {

        toastr.error("Произошла системная ошибка. Попробуйте позже.", "Ошибка");
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [id]);

  if (loading) return <p>Загрузка...</p>;
  if (error) return <p>Ошибка: {error}</p>;


  return (
    <div className="container">
      <div className="event-card">
        <h2>Основная информация о мероприятии «{data.event.name ?? ""}» </h2>
        <h2>Организатор: {data.event.organizer.name} ({data.event.organizer.municipality})</h2>
        <section>
          <label>Номер темы: {data.event.theme.code}</label>
          {/* <p></p> */}
        </section>

        <section>
          <label>Описание темы</label>
          <p>{data.event.theme.description}</p>
        </section>


        <section>
          <label>Структурная организация</label>
          <p>{data.event.organizer.name} ({data.event.organizer.municipality})</p>
        </section>


        {data.event.actor && (
          <section>
            <label>Исполнитель</label>
            <p>{data.event.actor}</p>
          </section>
        )}


        {data.event.name && (
          <section>
            <label>Наименование мероприятия</label>
            <p>{data.event.name}</p>
          </section>
        )}


        {data.event.date && (
          <section>
            <label>Дата проведения</label>
            <p>{data.event.date}</p>
          </section>
        )}


        {data.event.content && (
          <section>
            <label>Краткое описание мероприятия</label>
            <p>{data.event.content}</p>
          </section>
        )}


        {data.event.levelType && (
          <section>
            <label>Уровень мероприятия</label>
            <p>{data.event.levelType}</p>
          </section>
        )}

        {data.event.eventType && (
          <section>
            <label>Форма проведения</label>
            <p>{data.event.eventType}</p>
          </section>
        )}

        {data.event.decision && (
          <section>
            <label>Необходимые управленческие решения</label>
            <p>{data.event.decision}</p>
          </section>
        )}

        {data.event.result && (
          <section>
            <label>Результат мероприятия</label>
            <p>{data.event.result}</p>
          </section>
        )}

        {data.event.mediaLinks && data.event.mediaLinks.length > 0 && (
          <section>
            <label>Ссылка на СМИ/СМК</label>

            {data.event.mediaLinks.map((link) => (
              <tr key={link.id}>

                {link.organizationName && (
                  <label>СМИ: {link.organizationName}</label>
                )}
                <a href={link.content} target="_blank" rel="noopener noreferrer">{link.content}</a>
              </tr>
            ))}

          </section>
        )}


        {data.event.categories && data.event.categories.length > 0 && (
          <section>
            <h2>Участники мероприятия</h2>
            {data.event.categories.map((category) => (
              <tr key={category.id}>
                <p><strong>{category.name}: </strong>{category.count}</p>
              </tr>
            ))}
          </section>
        )}


        {data.event.finance && (
          <section>
            <h2>Финансирование</h2>
            <p><strong>Муниципальный бюджет:</strong> {data.event.finance?.municipalBudget} руб.</p>
            <p><strong>Окружной бюджет:</strong> {data.event.finance?.regionalBudget} руб.</p>
            <p><strong>Гранты/Субсидии:</strong> {data.event.finance?.granteBudget} руб.</p>
            <p><strong>Другое:</strong> {data.event.finance?.otherBudget} руб.</p>
            <p><strong>ИТОГО: {data.event.finance?.total} руб. </strong></p>
          </section>
        )}


        {data.event.feedBack && (
          <section>
            <h2>Обратная связь</h2>
            <p><strong>Опрос:</strong> {data.event.feedBack.hasOpros ? "Да" : "Нет"}</p>
            <p><strong>Онлайн-опрос:</strong> {data.event.feedBack.hasInternet ? "Да" : "Нет"}</p>
            <p><strong>Анкетирование:</strong> {data.event.feedBack.hasGuestionnaire ? "Да" : "Нет"}</p>
            <p><strong>Интервью:</strong> {data.event.feedBack.hasInterview ? "Да" : "Нет"}</p>
            <p><strong>Другое:</strong> {data.event.feedBack.hasOther ? "Да" : "Нет"}</p>
            <p><strong>Описание:</strong> {data.event.feedBack.description}</p>
          </section>
        )}


        {data.event.interAgencyCooperations && data.event.interAgencyCooperations.length > 0 && (
          <section>
            <h2>Взаимодействие</h2>
            <table style={{ color: "black", border: "1px black solid" }}>
              <thead style={{ color: "black", border: "1px black solid" }}>
                <tr style={{ color: "black", border: "1px black solid" }}>
                  <td style={{ color: "black", border: "1px black solid" }}>Организация</td>
                  <td style={{ color: "black", border: "1px black solid" }}>Тип участия</td>
                  <td style={{ color: "black", border: "1px black solid" }}>Описание</td>
                </tr>
              </thead>
              <tbody style={{ color: "black", border: "1px black solid" }}>
                {data.event.interAgencyCooperations.map((element) => (
                  <tr key={element.id} style={{ color: "black", border: "1px black solid" }}>
                    <td style={{ color: "black", border: "1px black solid" }}>{element.organization}</td>
                    <td style={{ color: "black", border: "1px black solid" }}>{element.role}</td>
                    <td style={{ color: "black", border: "1px black solid" }}>{element.description}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </section>
        )}


        {data.event.supports && data.event.supports.length > 0 && (
          <section>

            <h2>Поддержка</h2>

            <p><strong>Получатель поддержки: </strong>{data.event.supports[0].receiver}</p>

            {data.event.supports.map((support) => (
              <tr key={support.id}>
                <p><strong>{support.supportType}: </strong>{support.description}</p>
              </tr>
            ))}
          </section>
        )}


        {data.event.audiences && data.event.audiences.length > 0 && (
          <section>
            <h2>Целевая аудитория</h2>
            {data.event.audiences.map((audience) => (
              <tr key={audience.id}>
                <p><strong>  {audience.category}</strong></p>
              </tr>
            ))}
          </section>
        )}


        {data.event.theme.code === "1.3.5" && (
          <section>
            <h2>Отправка материала</h2>
            <p><strong>Отправка в НАК: {data.event.directToNAC ? "Да" : "Нет"}</strong></p>
            <p><strong>Отправка в субъекты: {data.event.directToSubjects ?? "Нет"}</strong></p>
          </section>
        )}


        {data.event.agreements && data.event.agreements.length > 0 && (
          <section>
            <h2>Согласования</h2>
            {data.event.agreements.map((agreement) => (
              <tr key={agreement.id}>
                <p><strong>Организация: {agreement.organization}</strong></p>
                <p><strong>Результат: {agreement.result}</strong></p>
                <p><strong>Описание: {agreement.description ?? "Нет"}</strong></p>
              </tr>
            ))}
          </section>
        )}


        {data.event.violations && data.event.violations.length > 0 && (
          <section>
            <h2>Выявленные нарушения и блокировки</h2>
            {data.event.violations.map((violation) => (
              <tr key={violation.id}>
                <p><strong>Орган власти или статья: {violation.name}</strong></p>
                <p><strong>Количество заблокированных/отправленных материалов: {violation.blocked}/{violation.send}</strong></p>

                {violation.order && (
                  <p><strong>Соответствующий приказ: {violation.order}</strong></p>
                )}
              </tr>
            ))}
          </section>
        )}


        {data.event.concourse && (
          <section>
            <h2>Направление на участие в конкурсе</h2>
            <p><strong>Название/описание конкурса: </strong> {data.event.concourse.description}</p>
            <p><strong>Результат конкурса: </strong> {data.event.concourse.result}</p>
            <p><strong>Описание результата конкурса: </strong> {data.event.concourse.details ?? ""}</p>
          </section>
        )}


        <section>
          <h2>Дополнительные характеристики</h2>
          <p><strong>Значимое мероприятие: </strong> {data.event.isValuable ? "Да" : "Нет"}</p>
          <p><strong>Включено в сборник лучших практик: </strong> {data.event.isBestPractice ? "Да" : "Нет"}</p>
          <p><strong>Формат равный равному: </strong> {data.event.equalToEqualDescription ?? "Нет"}</p>
        </section>


        {data.canDelete && (
          <>
            <button className="delete" type="button" onClick={() => setShowModal(true)}>Удалить</button>
            <DeleteConfirmationModal
              isOpen={showModal}
              onConfirm={handleDelete}
              onCancel={() => setShowModal(false)}
            />
          </>
        )}

        <button className="back" type="button" onClick={() => window.history.back()}>Назад</button>
      </div>
    </div>
  );
};

export default EventCard;
