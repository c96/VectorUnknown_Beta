using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class tutorial : MonoBehaviour
{

    public TextMeshProUGUI instruction, title;
    private string[] tutorial_messages = new string[7]
    {
        "\tmessage 1 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla bibendum quis nunc vitae rhoncus. Donec dignissim sollicitudin elit, luctus facilisis erat.Nam sagittis sed lorem eget viverra.onec malesuada eu nibh at egestas.Vestibulum metus nunc, euismod a vulputate eu, finibus vitae leo.Etiam vel interdum justo.Vestibulum feugiat quam quis odio aliquam facilisis.Ut sagittis felis sit amet nisi ornare, et vestibulum augue viverra.Donec dignissim auctor felis non tempus.Proin id massa lacinia, mattis dolor et, dignissim turpis.Donec ut leo ac enim commodo consectetur nec nec neque.Nunc ac ornare mauris, eu suscipit orci.Donec vel ex vitae odio dignissim fringilla vel ac lectus.Duis dignissim magna lorem, vel convallis eros rutrum id.Fusce sed nunc ac nisl aliquet tincidunt at vel arcu.Maecenas dignissim leo posuere, luctus lacus at, convallis justo.",
        "\tmessage 2",
        "\tmessage 3",
        "\tmessage 4",
        "\tmessage 5",
        "\tmessage 6",
        "\tmessage 7",
    };
    private string[] tutorial_titles = new string[7]
    {
        "1. Minimap",
        "2. Choice Vectors",
        "3. Player Info",
        "4. Log",
        "5. Formula Bar",
        "6. Timer",
        "7. Options",
    };

    private int current_message = 0;

    void Start()
    {
        set_msg();
    }
    private void set_msg()
    {
        instruction.text = tutorial_messages[current_message];
        title.text = tutorial_titles[current_message];
    }

    public void next_message()
    {
        if (current_message >= 6)
            return;

        current_message++;
        set_msg();
    }

    public void prev_message()
    {
        if (current_message <= 0)
            return;

        current_message--;
        set_msg();
    }
}
