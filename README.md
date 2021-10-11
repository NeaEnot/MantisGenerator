# MantisGenerator
Чуть более крутой генератор случайных чисел для выбора чего-либо с учётом иерархии

----

## RU

Допустим вы любите что-то готовить, но не очень любите выбирать что именно. Первая мысль, которая приходит в голову в этом случае - использовать генератор случайных чисел.

Усложним задачу. Допустим основные блюда вы предпочитаете готовить значительно чаще чем, скажем, салаты или напитки. Более того, среди основных блюд  вы чаще хотите варить борщ, нежели тушить овощи. В обычном генераторе случайных чисел вы можете для каждого из пунктов указать диапазон чисел (или несколько строчек), но в таком случае со временем становиться сложно изменить что-то в схеме.

В данном случае было бы удобнее использовать древовидную структуру, для каждого узла которой можно было бы задавать долю вероятности его выпадения. Именно этот функционал и предлагает представленное приложение.

Для создания узла в корне дерева необходимо нажать на кнопку "Создать". Для создания дочернего узла необходимо выделить родительский узел и нажать на кнопку "Создать". Для удаления узла и всех его потомков необходимо выделить интересующий узел и нажать "Удалить". При создании/редактировании узла задаётся его имя и доля вероятности его выпадения (можно задать 0 для отключения). Вся информация сохраняется в файле storage.json в каталоге программы.

Скачать приложение можно по [ссылке](https://disk.yandex.ru/d/WPVKl2FdgsYT-g).

----

## EN

Let's say you like to cook something, but you don't really like to choose what it is. The first thought that comes to mind in this case is to use a random number generator.

Let's complicate the task. Let's say you prefer to cook the main dishes much more often than, say, salads or drinks. Moreover, among the main dishes, you often want to cook borscht rather than stew vegetables. In a regular random number generator, you can specify a range of numbers (or several lines) for each of the items, but in this case it becomes difficult to change something in the scheme over time.

In this case, it would be more convenient to use a tree structure, for each node of which it would be possible to set the probability fraction of its choice. It is this functionality that the presented application offers.

To create a node at the root of the tree, click on the "Create" button. To create a child node, select the parent node and click on the "Create" button. To delete a node and all its descendants, select the node of interest and click "Delete". When creating/editing a node, its name and the probability of its selection are set (you can set 0 to disable it). All information is stored in the storage file.json in the program directory.

You can download the application from the [link](https://disk.yandex.ru/d/WPVKl2FdgsYT-g).
